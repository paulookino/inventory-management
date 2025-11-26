using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category> CreateCategoryAsync(string name, string shortcode, Guid? parentCategoryId);
		Task DeleteAsync(Guid id);
	}
}
