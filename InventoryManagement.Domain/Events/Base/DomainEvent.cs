namespace InventoryManagement.Domain.Events.Base
{
	public abstract class DomainEvent
	{
		public DateTime OccurredOn { get; } = DateTime.UtcNow;
	}
}
