using System.Text.RegularExpressions;

namespace InventoryManagement.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
	public string Value { get; }

	private static readonly Regex _regex = new(
		@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
		RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	private Email() { }

	public Email(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException("Email cannot be empty.");

		value = value.Trim().ToLowerInvariant();

		if (!_regex.IsMatch(value))
			throw new ArgumentException("Invalid email format.");

		Value = value;
	}

	public override string ToString() => Value;

	public bool Equals(Email? other)
	{
		if (other is null) return false;

		return Value == other.Value;
	}

	public override bool Equals(object? obj) => Equals(obj as Email);

	public override int GetHashCode() => Value.GetHashCode();
}
