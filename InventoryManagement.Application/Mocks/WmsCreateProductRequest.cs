namespace InventoryManagement.Application.Mocks
{
	public class WmsCreateProductRequest
	{
		public string ProductId { get; }
		public string Description { get; }
		public string CategoryShortcode { get; }
		public Guid SupplierId { get; }


		public WmsCreateProductRequest(string productId, string description, string categoryShortcode, Guid supplierId)
		{
			ProductId = productId;
			Description = description;
			CategoryShortcode = categoryShortcode;
			SupplierId = supplierId;
		}
	}
}
