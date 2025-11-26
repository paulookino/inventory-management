using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces.Repositories
{
	public interface IProductRepository
	{
		Task<Product?> GetByIdAsync(Guid id);
		Task<List<Product>> GetAllAsync();
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
		Task<bool> ExistsAsync(Guid id);
	}
}
