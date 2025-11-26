namespace InventoryManagement.API.Dtos
{
	public class CreateProductRequest
	{
		public Guid SupplierId { get; set; }
		public Guid CategoryId { get; set; }
		public string Description { get; set; }
		public decimal AcquisitionCostSupplierCurrency { get; set; }
		public decimal AcquisitionCostUSD { get; set; }
		public DateTime AcquireDate { get; set; }
		public Guid UserId { get; set; }
		public string UserEmail { get; set; }
	}
}
