using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Mocks;
using InventoryManagement.Application.Services;
using InventoryManagement.Application.Services.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces.Repositories;
using Moq;

namespace InventoryManagement.Tests.Application;

public class ProductServiceTests
{
	private readonly Mock<IProductRepository> _repo = new();
	private readonly Mock<ISupplierRepository> _supplierRepo = new();
	private readonly Mock<ICategoryRepository> _categoryRepo = new();
	private readonly Mock<IWmsClient> _wms = new();
	private readonly Mock<IAuditClient> _audit = new();
	private readonly Mock<IEmailSender> _email = new();

	private readonly ProductService _service;

	public ProductServiceTests()
	{
		_service = new ProductService(_repo.Object, _categoryRepo.Object, _supplierRepo.Object, _wms.Object, _audit.Object, _email.Object);
	}

	[Fact]
	public async Task CreateProductAsync_Should_Save_And_Call_WMS()
	{
		// Arrange
		var supplierId = Guid.NewGuid();
		var categoryId = Guid.NewGuid();

		var supplier = new Supplier("Test Supplier", "supplier@test.com", "BRL", "BR");
		var category = new Category("Electronics", "ELEC");

		_supplierRepo.Setup(x => x.GetByIdAsync(supplierId)).ReturnsAsync(supplier);
		_categoryRepo.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
		_wms.Setup(x => x.CreateProductAsync(It.IsAny<WmsCreateProductRequest>())).ReturnsAsync("WMS-777");

		var cmd = new CreateProductCommand
		{
			SupplierId = supplierId,
			CategoryId = categoryId,
			Description = "Test product",
			AcquisitionCostSupplierCurrency = 50,
			AcquisitionCostUSD = 10
		};

		// Act
		var product = await _service.CreateProductAsync(cmd);

		// Assert
		Assert.NotEqual(Guid.Empty, product.Id);
		_repo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
		_wms.Verify(x => x.CreateProductAsync(It.IsAny<WmsCreateProductRequest>()), Times.Once);
		_audit.Verify(x => x.CreateLogAsync(It.IsAny<AuditLogEntry>()), Times.Once);
	}

	[Fact]
	public async Task ChangeStatusAsync_Should_Update_Status_And_Call_Dispatch()
	{
		// Arrange
		var product = new Product(
			Guid.NewGuid(),
			Guid.NewGuid(),
			"Test Product Description",
			10,
			11,
			DateTime.UtcNow
		);

		product.SetWmsProductId("WMS-888");

		_repo.Setup(x => x.GetByIdAsync(product.Id)).ReturnsAsync(product);

		var cmd = new ChangeProductStatusCommand
		{
			ProductId = product.Id,
			NewStatus = ProductStatus.Sold,
			StatusDate = DateTime.UtcNow
		};

		// Act
		await _service.ChangeProductStatusAsync(cmd);

		// Assert
		Assert.Equal(ProductStatus.Sold, product.Status);
		_wms.Verify(x => x.DispatchProductAsync("WMS-888"), Times.Once);
		_audit.Verify(x => x.CreateLogAsync(It.IsAny<AuditLogEntry>()), Times.Once);
	}
}
