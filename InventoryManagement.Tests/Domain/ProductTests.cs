using System;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.ValueObjects;
using InventoryManagement.Domain.Enums;
using Xunit;

namespace InventoryManagement.Tests.Domain;

public class ProductTests
{
	[Fact]
	public void MarkAsSold_Should_Set_Status_And_Date()
	{
		// Arrange
		var product = CreateProduct();

		// Act
		var now = DateTime.UtcNow;
		product.MarkAsSold(now);

		// Assert
		Assert.Equal(ProductStatus.Sold, product.Status);
		Assert.Equal(now, product.SoldDate);
	}

	[Fact]
	public void MarkAsSold_Should_Throw_When_Cancelled()
	{
		var product = CreateProduct();
		product.MarkAsCancelled(DateTime.UtcNow);

		Assert.Throws<InvalidOperationException>(() =>
			product.MarkAsSold(DateTime.UtcNow));
	}

	[Fact]
	public void MarkAsCancelled_Should_Set_Status_And_Date()
	{
		var product = CreateProduct();
		var now = DateTime.UtcNow;

		product.MarkAsCancelled(now);

		Assert.Equal(ProductStatus.Cancelled, product.Status);
		Assert.Equal(now, product.CancelDate);
	}

	[Fact]
	public void MarkAsReturned_Should_Set_Status_And_Date()
	{
		var product = CreateProduct();
		var now = DateTime.UtcNow;

		product.MarkAsReturned(now);

		Assert.Equal(ProductStatus.Returned, product.Status);
		Assert.Equal(now, product.ReturnDate);
	}

	[Fact]
	public void SetWmsProductId_Should_Set_Properly()
	{
		var product = CreateProduct();

		product.SetWmsProductId("WMS-123");

		Assert.Equal("WMS-123", product.WmsProductId);
	}

	private Product CreateProduct()
	{
		return new Product(
			Guid.NewGuid(),
			Guid.NewGuid(),
			"Test Product Description",
			10,
			11,
			DateTime.UtcNow
		);
	}
}
