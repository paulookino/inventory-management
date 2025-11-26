namespace InventoryManagement.Application.Mocks
{
	public class AuditLogEntry	
	{
		public Guid UserId { get; }
		public string Email { get; }
		public string ActionName { get; }
		public DateTime Timestamp { get; }


		public AuditLogEntry(Guid userId, string email, string actionName, DateTime timestamp)
		{
			UserId = userId;
			Email = email;
			ActionName = actionName;
			Timestamp = timestamp;
		}
	}
}
