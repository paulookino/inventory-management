using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrasctructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly InventoryDbContext _context;


		public ProductRepository(InventoryDbContext context) => _context = context;


		public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);


		public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();


		public async Task<Product?> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);


		public async Task UpdateAsync(Product product) => _context.Products.Update(product);


		public async Task<bool> ExistsAsync(Guid id) => await _context.Products.AnyAsync(p => p.Id == id);
	}
}
