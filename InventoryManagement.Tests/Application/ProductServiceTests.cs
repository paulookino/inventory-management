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
	private readonly Mock<ISupplierRepository> _suppllierRepo = new();
	private readonly Mock<ICategoryRepository> _categoryRepo = new();
	private readonly Mock<IWmsClient> _wms = new();
	private readonly Mock<IAuditClient> _audit = new();
	private readonly Mock<IEmailSender> _email = new();

	private readonly ProductService _service;

	public ProductServiceTests()
	{
		_service = new ProductService(_repo.Object, _categoryRepo.Object, _suppllierRepo.Object, _wms.Object, _audit.Object, _email.Object);
	}

	[Fact]
	public async Task CreateProductAsync_Should_Save_And_Call_WMS()
	{
		// Arrange
		var cmd = new CreateProductCommand
		{
			SupplierId = Guid.NewGuid(),
			CategoryId = Guid.NewGuid(),
			Description = "Test product",
			AcquisitionCostSupplierCurrency = 50,
			AcquisitionCostUSD = 10
		};

		_wms.Setup(x => x.CreateProductAsync(It.IsAny<WmsCreateProductRequest>()))
			.ReturnsAsync("WMS-777");

		// Act
		var product = await _service.CreateProductAsync(cmd);

		// Assert
		_repo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
		_wms.Verify(x => x.CreateProductAsync(It.IsAny<WmsCreateProductRequest>()), Times.Once);
		_audit.Verify(x => x.CreateLogAsync(It.IsAny<AuditLogEntry>()), Times.Once);

		Assert.NotEqual(Guid.Empty, product.Id);
	}

	[Fact]
	public async Task ChangeStatusAsync_Should_Update_Status_And_Call_Dispatch()
	{
		// Arrange
		var product = new Product(
			Guid.NewGuid(),
			Guid.NewGuid(),
			"",
			10,
			11,
			DateTime.UtcNow
		);

		product.SetWmsProductId("WMS-888");

		_repo.Setup(x => x.GetByIdAsync(product.Id)).ReturnsAsync(product);

		var cmd = new ChangeProductStatusCommand
		{
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
