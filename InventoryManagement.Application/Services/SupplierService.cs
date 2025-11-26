using InventoryManagement.Application.Services.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces.Repositories;

namespace InventoryManagement.Application.Services;

public class SupplierService : ISupplierService
{
	private readonly ISupplierRepository _supplierRepository;
	private readonly IUnitOfWork _unitOfWork;

	public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
	{
		_supplierRepository = supplierRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Supplier> CreateSupplierAsync(string name, string email, string currency, string country)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Supplier name is required.");

		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Supplier email is required.");

		if (string.IsNullOrWhiteSpace(currency))
			throw new ArgumentException("Supplier currency is required.");

		if (string.IsNullOrWhiteSpace(country))
			throw new ArgumentException("Supplier country is required.");

		var supplier = new Supplier(name, email, currency, country);
		await _supplierRepository.AddAsync(supplier);
		await _unitOfWork.SaveChangesAsync();

		return supplier;
	}

	public async Task<IEnumerable<Supplier>> GetAllAsync()
	{
		return await _supplierRepository.GetAllAsync();
	}
}