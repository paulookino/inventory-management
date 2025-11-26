namespace InventoryManagement.Domain.ValueObjects;

public sealed class Country : IEquatable<Country>
{
	public string Code { get; }

	private Country() { }

	public Country(string code)
	{
		if (string.IsNullOrWhiteSpace(code))
			throw new ArgumentException("Country code cannot be empty.");

		code = code.Trim().ToUpperInvariant();

		if (code.Length != 2)
			throw new ArgumentException("Country code must follow ISO 3166-1 alpha-2 (2 letters), e.g., US, BR, PT.");

		Code = code;
	}

	public override string ToString() => Code;

	public bool Equals(Country? other)
	{
		if (other is null) return false;
		return Code == other.Code;
	}

	public override bool Equals(object? obj) => Equals(obj as Country);

	public override int GetHashCode() => Code.GetHashCode();
}