using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly InventoryDbContext _context;

		public CategoryRepository(InventoryDbContext context) => _context = context;

		public async Task AddAsync(Category category) => await _context.Categories.AddAsync(category);

		public async Task DeleteAsync(Category category) => _context.Categories.Remove(category);

		public async Task<bool> ExistsByShortcodeAsync(string shortcode) =>
		await _context.Categories.AnyAsync(c => c.Shortcode == shortcode);

		public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

		public async Task<Category?> GetByIdAsync(Guid id) => await _context.Categories.FindAsync(id);
	}
}
