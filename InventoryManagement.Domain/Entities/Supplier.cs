namespace InventoryManagement.Domain.Entities;

public class Supplier
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Email { get; private set; }
	public string Currency { get; private set; }
	public string Country { get; private set; }

	private Supplier() { }

	public Supplier(string name, string email, string currency, string country)
	{
		Id = Guid.NewGuid();
		SetName(name);
		SetEmail(email);
		SetCurrency(currency);
		SetCountry(country);
	}

	public void SetName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be empty", nameof(name));

		Name = name.Trim();
	}

	public void SetEmail(string email)
	{
		if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
			throw new ArgumentException("Invalid email format", nameof(email));

		Email = email.Trim().ToLowerInvariant();
	}

	public void SetCurrency(string currency)
	{
		if (string.IsNullOrWhiteSpace(currency))
			throw new ArgumentException("Currency cannot be empty", nameof(currency));

		Currency = currency.Trim().ToUpperInvariant();
	}

	public void SetCountry(string country)
	{
		if (string.IsNullOrWhiteSpace(country))
			throw new ArgumentException("Country cannot be empty", nameof(country));

		Country = country.Trim();
	}
}
