namespace InventoryManagement.Domain.ValueObjects;

public sealed class ProductDescription : IEquatable<ProductDescription>
{
	public string Value { get; }

	private ProductDescription() { }

	public ProductDescription(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException("Description cannot be empty.");

		value = value.Trim();

		if (value.Length < 3)
			throw new ArgumentException("Description must be at least 3 characters long.");

		if (value.Length > 250)
			throw new ArgumentException("Description cannot exceed 250 characters.");

		Value = value;
	}

	public override string ToString() => Value;

	public bool Equals(ProductDescription? other)
	{
		if (other is null) return false;
		return Value == other.Value;
	}

	public override bool Equals(object? obj) => Equals(obj as ProductDescription);

	public override int GetHashCode() => Value.GetHashCode();
}
