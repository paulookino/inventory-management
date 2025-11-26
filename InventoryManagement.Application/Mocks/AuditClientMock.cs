namespace InventoryManagement.Application.Mocks
{
	public class AuditClientMock : IAuditClient
	{
		public Task CreateLogAsync(AuditLogEntry entry)
		{
			Console.WriteLine($"[AUDIT MOCK] {entry.Timestamp:u} - User {entry.UserId} performed {entry.ActionName}");
			return Task.CompletedTask;
		}
	}
}
