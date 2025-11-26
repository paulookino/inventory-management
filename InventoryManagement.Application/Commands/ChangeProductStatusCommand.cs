using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Commands
{
	public class ChangeProductStatusCommand
	{
		public Guid ProductId { get; set; }
		public ProductStatus NewStatus { get; set; }
		public DateTime StatusDate { get; set; }
		public Guid UserId { get; set; }
		public string UserEmail { get; set; } = null!;
	}
}
