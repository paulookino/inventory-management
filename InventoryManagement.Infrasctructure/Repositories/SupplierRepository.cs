using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrasctructure.Repositories
{
	public class SupplierRepository : ISupplierRepository
	{
		private readonly InventoryDbContext _context;

		public SupplierRepository(InventoryDbContext context) => _context = context;

		public async Task AddAsync(Supplier supplier) => await _context.Suppliers.AddAsync(supplier);

		public async Task<List<Supplier>> GetAllAsync() => await _context.Suppliers.ToListAsync();

		public async Task<Supplier?> GetByIdAsync(Guid id) => await _context.Suppliers.FindAsync(id);
	}
}
