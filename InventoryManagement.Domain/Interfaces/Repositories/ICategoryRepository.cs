using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces.Repositories
{
	public interface ICategoryRepository
	{
		Task<Category?> GetByIdAsync(Guid id);
		Task<List<Category>> GetAllAsync();
		Task AddAsync(Category category);
		Task DeleteAsync(Category category);
		Task<bool> ExistsByShortcodeAsync(string shortcode);
	}
}
