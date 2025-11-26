using InventoryManagement.API.Dtos;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
	private readonly CategoryService _service;


	public CategoriesController(CategoryService service)
	{
		_service = service;
	}


	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var result = await _service.GetAllAsync();
		return Ok(result);
	}


	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
	{
		var id = await _service.CreateCategoryAsync(request.Name, request.Shortcode, request.ParentCategoryId);
		return CreatedAtAction(nameof(GetAll), new { id }, null);
	}


	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await _service.DeleteAsync(id);
		return Ok();
	}
}