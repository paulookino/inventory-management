using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services.Interfaces
{
	public interface ISupplierService
	{
		Task<Supplier> CreateSupplierAsync(string name, string email, string currency, string country);
		Task<IEnumerable<Supplier>> GetAllAsync();
	}
}
