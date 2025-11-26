using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities;

public class Product
{
	public Guid Id { get; private set; }
	public Guid SupplierId { get; private set; }
	public Guid CategoryId { get; private set; }
	public string? WmsProductId { get; private set; }

	public string Description { get; private set; }
	public decimal AcquisitionCostSupplierCurrency { get; private set; }
	public decimal AcquisitionCostUSD { get; private set; }

	public DateTime AcquireDate { get; private set; }
	public DateTime? SoldDate { get; private set; }
	public DateTime? CancelDate { get; private set; }
	public DateTime? ReturnDate { get; private set; }

	public ProductStatus Status { get; private set; }

	private Product() { }

	public Product(
		Guid supplierId,
		Guid categoryId,
		string description,
		decimal acquisitionCostSupplierCurrency,
		decimal acquisitionCostUSD,
		DateTime acquireDate)
	{
		Id = Guid.NewGuid();
		SupplierId = supplierId;
		CategoryId = categoryId;
		SetDescription(description);
		SetAcquisitionCosts(acquisitionCostSupplierCurrency, acquisitionCostUSD);
		AcquireDate = acquireDate;
		Status = ProductStatus.Created;
	}

	public void SetDescription(string description)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new ArgumentException("Description cannot be empty", nameof(description));

		Description = description.Trim();
	}

	public void SetAcquisitionCosts(decimal supplierCurrencyCost, decimal usdCost)
	{
		if (supplierCurrencyCost <= 0)
			throw new ArgumentException("Acquisition cost in supplier currency must be greater than 0");

		if (usdCost <= 0)
			throw new ArgumentException("Acquisition cost in USD must be greater than 0");

		AcquisitionCostSupplierCurrency = supplierCurrencyCost;
		AcquisitionCostUSD = usdCost;
	}

	public void MarkAsSold(DateTime soldDate)
	{
		if (Status == ProductStatus.Cancelled || Status == ProductStatus.Returned)
			throw new InvalidOperationException("Cancelled or returned products cannot be sold.");

		Status = ProductStatus.Sold;
		SoldDate = soldDate;
	}

	public void MarkAsCancelled(DateTime cancelledDate)
	{
		if (Status == ProductStatus.Returned)
			throw new InvalidOperationException("Returned products cannot be cancelled.");

		Status = ProductStatus.Cancelled;
		CancelDate = cancelledDate;
	}

	public void MarkAsReturned(DateTime returnDate)
	{
		if (Status == ProductStatus.Cancelled)
			throw new InvalidOperationException("Cancelled products cannot be returned.");

		if (Status == ProductStatus.Returned)
			throw new InvalidOperationException("Product has already been returned.");

		Status = ProductStatus.Returned;
		ReturnDate = returnDate;
	}

	public void SetWmsProductId(string wmsId)
	{
		if (string.IsNullOrWhiteSpace(wmsId))
			throw new ArgumentException("WMS product ID cannot be null or empty.");


		WmsProductId = wmsId;
	}

}