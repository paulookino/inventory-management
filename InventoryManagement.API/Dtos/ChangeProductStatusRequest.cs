using InventoryManagement.Domain.Enums;

namespace InventoryManagement.API.Dtos
{
	public class ChangeProductStatusRequest
	{
		public Guid ProductId { get; set; }
		public ProductStatus NewStatus { get; set; }
		public DateTime StatusDate { get; set; }
		public Guid UserId { get; set; }
		public string UserEmail { get; set; }
	}
}
