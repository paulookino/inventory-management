using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Mocks;
using InventoryManagement.Application.Services.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces.Repositories;

namespace InventoryManagement.Application.Services;

public class ProductService : IProductService
{
	private readonly IProductRepository _productRepository;
	private readonly ICategoryRepository _categoryRepository;
	private readonly ISupplierRepository _supplierRepository;
	private readonly IWmsClient _wmsClient;
	private readonly IAuditClient _auditClient;
	private readonly IEmailSender _emailSender;

	public ProductService(
		IProductRepository productRepository,
		ICategoryRepository categoryRepository,
		ISupplierRepository supplierRepository,
		IWmsClient wmsClient,
		IAuditClient auditClient,
		IEmailSender emailSender)
	{
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
		_supplierRepository = supplierRepository;
		_wmsClient = wmsClient;
		_auditClient = auditClient;
		_emailSender = emailSender;
	}

	public async Task<Product> CreateProductAsync(CreateProductCommand command)
	{
		var supplier = await _supplierRepository.GetByIdAsync(command.SupplierId)
			?? throw new InvalidOperationException("Supplier not found");
		var category = await _categoryRepository.GetByIdAsync(command.CategoryId)
			?? throw new InvalidOperationException("Category not found");

		var product = new Product(
			command.SupplierId,
			command.CategoryId,
			command.Description,
			command.AcquisitionCostSupplierCurrency,
			command.AcquisitionCostUSD,
			command.AcquireDate);

		await _productRepository.AddAsync(product);

		var wmsRequest = new WmsCreateProductRequest(product.Id.ToString(), product.Description, category.Shortcode, supplier.Id);
		var wmsId = await _wmsClient.CreateProductAsync(wmsRequest);
		product.SetWmsProductId(wmsId);
		await _productRepository.UpdateAsync(product);

		var auditEntry = new AuditLogEntry(command.UserId, command.UserEmail, "PRODUCT_CREATED", DateTime.UtcNow);
		await _auditClient.CreateLogAsync(auditEntry);

		return product;
	}

	public async Task ChangeProductStatusAsync(ChangeProductStatusCommand command)
	{
		var product = await _productRepository.GetByIdAsync(command.ProductId)
			?? throw new InvalidOperationException("Product not found");

		var oldStatus = product.Status.ToString();

		switch (command.NewStatus)
		{
			case ProductStatus.Sold:
				product.MarkAsSold(DateTime.UtcNow);
				await _wmsClient.DispatchProductAsync(product.WmsProductId!);
				var supplier = await _supplierRepository.GetByIdAsync(product.SupplierId);
				if (supplier != null)
					await _emailSender.SendEmailAsync(supplier.Email, "Product Sold", $"Product {product.Description} sold.");
				break;
			case ProductStatus.Cancelled:
				product.MarkAsCancelled(DateTime.UtcNow);
				break;
			case ProductStatus.Returned:
				product.MarkAsReturned(DateTime.UtcNow);
				break;
			default:
				throw new InvalidOperationException("Invalid status");
		}

		await _productRepository.UpdateAsync(product);

		var auditEntry = new AuditLogEntry(command.UserId, command.UserEmail, $"PRODUCT_STATUS_CHANGED_FROM_{oldStatus}_TO_{product.Status}", DateTime.UtcNow);
		await _auditClient.CreateLogAsync(auditEntry);
	}

}
