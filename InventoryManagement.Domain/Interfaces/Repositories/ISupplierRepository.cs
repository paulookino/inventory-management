using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces.Repositories
{
	public interface ISupplierRepository
	{
		Task<Supplier?> GetByIdAsync(Guid id);
		Task<List<Supplier>> GetAllAsync();
		Task AddAsync(Supplier supplier);
	}
}
