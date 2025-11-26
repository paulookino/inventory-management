namespace InventoryManagement.Domain.Entities;

public class Category
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Shortcode { get; private set; }
	public Guid? ParentCategoryId { get; private set; }

	public Category? ParentCategory { get; private set; }

	private Category() { }

	public Category(string name, string shortcode, Guid? parentCategoryId = null)
	{
		Id = Guid.NewGuid();
		SetName(name);
		SetShortcode(shortcode);
		ParentCategoryId = parentCategoryId;
	}

	public void SetName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be empty", nameof(name));

		Name = name.Trim();
	}

	public void SetShortcode(string shortcode)
	{
		if (string.IsNullOrWhiteSpace(shortcode))
			throw new ArgumentException("Shortcode cannot be empty", nameof(shortcode));

		Shortcode = shortcode.Trim().ToUpperInvariant();
	}

	public void SetParent(Guid? parentId)
	{
		ParentCategoryId = parentId;
	}
}