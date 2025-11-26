namespace InventoryManagement.Domain.Interfaces.Repositories
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
