using InventoryManagement.API.Dtos;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManagement.API.Controllers;


[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
	private readonly SupplierService _supplierService;


	public SuppliersController(SupplierService supplierService)
	{
		_supplierService = supplierService;
	}


	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var suppliers = await _supplierService.GetAllAsync();
		return Ok(suppliers);
	}


	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
	{
			var supplier = await _supplierService.CreateSupplierAsync(
			request.Name,
			request.Email,
			request.Currency,
			request.Country
			);

			return Created($"/api/suppliers/{supplier.Id}", supplier);
	}
}