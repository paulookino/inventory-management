namespace InventoryManagement.Application.Mocks
{
	public class WmsClientMock : IWmsClient
	{
		public Task<string> CreateProductAsync(WmsCreateProductRequest request)
		{
			return Task.FromResult(Guid.NewGuid().ToString());
		}


		public Task DispatchProductAsync(string wmsProductId)
		{
			Console.WriteLine($"[WMS MOCK] Dispatch triggered for product {wmsProductId}");
			return Task.CompletedTask;
		}
	}
}
