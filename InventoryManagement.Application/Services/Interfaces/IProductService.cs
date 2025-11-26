using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services.Interfaces
{
	public interface IProductService
	{
		Task<Product> CreateProductAsync(CreateProductCommand command);
		Task ChangeProductStatusAsync(ChangeProductStatusCommand command);
	}
}
