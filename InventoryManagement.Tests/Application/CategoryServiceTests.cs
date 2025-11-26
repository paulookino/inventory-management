using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;
using Moq;


namespace InventoryManagement.Tests.Application;

public class CategoryServiceTests
{
	private readonly Mock<ICategoryRepository> _repo = new();
	private readonly Mock<IUnitOfWork> _unit = new();
	private readonly CategoryService _service;

	public CategoryServiceTests()
	{
		_service = new CategoryService(_repo.Object, _unit.Object);
	}

	[Fact]
	public async Task CreateCategory_Should_Save()
	{
		
		var category = await _service.CreateCategoryAsync("Electronics", "ELEC", Guid.NewGuid());

		_repo.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
		Assert.NotEqual(Guid.Empty, category.Id);
	}

	[Fact]
	public async Task GetAllCategories_Should_Return_List()
	{
		_repo.Setup(x => x.GetAllAsync())
			.ReturnsAsync(new List<Category>
			{
				new Category("Test", "TS", Guid.NewGuid())
			});

		var result = await _service.GetAllAsync();

		Assert.Single(result);
	}
}
