namespace InventoryManagement.Domain.ValueObjects;

public sealed class Currency : IEquatable<Currency>
{
	public string Code { get; }

	private Currency() { }

	public Currency(string code)
	{
		if (string.IsNullOrWhiteSpace(code))
			throw new ArgumentException("Currency code cannot be empty.");

		code = code.Trim().ToUpperInvariant();

		if (code.Length != 3)
			throw new ArgumentException("Currency code must follow ISO 4217 (3 letters), e.g., USD, EUR, BRL.");

		Code = code;
	}

	public override string ToString() => Code;

	public bool Equals(Currency? other)
	{
		if (other is null) return false;
		return Code == other.Code;
	}

	public override bool Equals(object? obj) => Equals(obj as Currency);

	public override int GetHashCode() => Code.GetHashCode();
}
