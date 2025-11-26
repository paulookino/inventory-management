using InventoryManagement.API.Dtos;
using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManagement.API.Controllers;


[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
	private readonly ProductService _productService;


	public ProductsController(ProductService productService)
	{
		_productService = productService;
	}


	// POST: /api/products
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
	{
		try
		{
			var command = new CreateProductCommand
			{
				SupplierId = request.SupplierId,
				CategoryId = request.CategoryId,
				Description = request.Description,
				AcquisitionCostSupplierCurrency = request.AcquisitionCostSupplierCurrency,
				AcquisitionCostUSD = request.AcquisitionCostUSD,
				AcquireDate = request.AcquireDate,
				UserId = request.UserId,
				UserEmail = request.UserEmail
			};


			var product = await _productService.CreateProductAsync(command);
			return Created($"/api/products/{product.Id}", product);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}


	// PATCH: /api/products/{id}/status
	[HttpPatch("{id}/status")]
	public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeProductStatusRequest request)
	{
		if (id != request.ProductId)
			return BadRequest(new { error = "ProductId mismatch." });

		var command = new ChangeProductStatusCommand
		{
			ProductId = request.ProductId,
			NewStatus = request.NewStatus,
			StatusDate = request.StatusDate,
			UserId = request.UserId,
			UserEmail = request.UserEmail
		};

		await _productService.ChangeProductStatusAsync(command);
		return Ok();
	}
}