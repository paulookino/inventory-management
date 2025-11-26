using InventoryManagement.Application.Services.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;

namespace InventoryManagement.Application.Services;

public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
	{
		_categoryRepository = categoryRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Category> CreateCategoryAsync(string name, string shortcode, Guid? parentCategoryId)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Category name is required.");

		if (string.IsNullOrWhiteSpace(shortcode))
			throw new ArgumentException("Category shortcode is required.");

		var exists = await _categoryRepository.ExistsByShortcodeAsync(shortcode);
		if (exists)
			throw new InvalidOperationException($"Category shortcode '{shortcode}' already exists.");

		var category = new Category(name, shortcode, parentCategoryId);

		await _categoryRepository.AddAsync(category);
		await _unitOfWork.SaveChangesAsync();

		return category;
	}

	public async Task<IEnumerable<Category>> GetAllAsync()
	{
		return await _categoryRepository.GetAllAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var category = await _categoryRepository.GetByIdAsync(id);
		if (category == null)
			throw new KeyNotFoundException("Category not found.");

		await _categoryRepository.DeleteAsync(category);
		await _unitOfWork.SaveChangesAsync();
	}
}
