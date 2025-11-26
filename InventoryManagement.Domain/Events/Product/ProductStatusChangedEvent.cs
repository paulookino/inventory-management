using InventoryManagement.Domain.Events.Base;

namespace InventoryManagement.Domain.Events.Product
{
	public class ProductStatusChangedEvent : DomainEvent
	{
		public Guid ProductId { get; }
		public string OldStatus { get; }
		public string NewStatus { get; }


		public ProductStatusChangedEvent(Guid productId, string oldStatus, string newStatus)
		{
			ProductId = productId;
			OldStatus = oldStatus;
			NewStatus = newStatus;
		}
	}
}
