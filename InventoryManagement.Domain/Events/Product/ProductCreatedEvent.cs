using InventoryManagement.Domain.Events.Base;

namespace InventoryManagement.Domain.Events.Product
{
	public class ProductCreatedEvent : DomainEvent
	{
		public Guid ProductId { get; }


		public ProductCreatedEvent(Guid productId)
		{
			ProductId = productId;
		}
	}
}
