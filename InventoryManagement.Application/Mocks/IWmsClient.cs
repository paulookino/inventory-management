namespace InventoryManagement.Application.Mocks
{
	public interface IWmsClient
	{
		Task<string> CreateProductAsync(WmsCreateProductRequest request);
		Task DispatchProductAsync(string wmsProductId);
	}
}
