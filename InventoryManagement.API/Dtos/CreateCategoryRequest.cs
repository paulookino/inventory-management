namespace InventoryManagement.API.Dtos
{
	public class CreateCategoryRequest
	{
		public string Name { get; set; } = string.Empty;
		public string Shortcode { get; set; } = string.Empty;
		public Guid? ParentCategoryId { get; set; }
	}
}
