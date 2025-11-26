namespace InventoryManagement.Application.Mocks
{
	public interface IAuditClient
	{
		Task CreateLogAsync(AuditLogEntry entry);
	}
}
