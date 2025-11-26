using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Commands;

public class CreateProductCommand
{
	public Guid SupplierId { get; set; }
	public Guid CategoryId { get; set; }
	public string Description { get; set; } = null!;
	public decimal AcquisitionCostSupplierCurrency { get; set; }
	public decimal AcquisitionCostUSD { get; set; }
	public DateTime AcquireDate { get; set; }

	public Guid UserId { get; set; }
	public string UserEmail { get; set; } = null!;
}
