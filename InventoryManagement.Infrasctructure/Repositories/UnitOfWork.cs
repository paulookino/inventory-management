using InventoryManagement.Domain.Interfaces.Repositories;
using InventoryManagement.Infrastructure;

namespace InventoryManagement.Infrasctructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly InventoryDbContext _context;

		public UnitOfWork(InventoryDbContext context) => _context = context;

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}
