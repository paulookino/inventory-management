namespace InventoryManagement.API.Dtos
{
	public class CreateSupplierRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Currency { get; set; }
		public string Country { get; set; }
	}
}
