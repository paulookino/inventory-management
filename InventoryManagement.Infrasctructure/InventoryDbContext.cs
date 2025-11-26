using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

public class InventoryDbContext : DbContext
{
	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

	public DbSet<Category> Categories { get; set; } = null!;
	public DbSet<Supplier> Suppliers { get; set; } = null!;
	public DbSet<Product> Products { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Category
		modelBuilder.Entity<Category>(entity =>
		{
			entity.HasKey(c => c.Id);
			entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
			entity.Property(c => c.Shortcode).IsRequired().HasMaxLength(10);
			entity.HasOne(c => c.ParentCategory).WithMany().HasForeignKey(c => c.ParentCategoryId);
		});

		// Supplier
		modelBuilder.Entity<Supplier>(entity =>
		{
			entity.HasKey(s => s.Id);
			entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
			entity.Property(s => s.Email).IsRequired().HasMaxLength(150);
			entity.Property(s => s.Currency).IsRequired().HasMaxLength(3);
			entity.Property(s => s.Country).IsRequired().HasMaxLength(2);
		});

		// Product
		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(p => p.Id);
			entity.Property(p => p.Description).IsRequired().HasMaxLength(250);
			entity.Property(p => p.AcquisitionCostSupplierCurrency).IsRequired();
			entity.Property(p => p.AcquisitionCostUSD).IsRequired();
			entity.Property(p => p.Status).IsRequired();
			entity.Property(p => p.AcquireDate).IsRequired();

			entity.HasOne<Category>().WithMany().HasForeignKey(p => p.CategoryId);
			entity.HasOne<Supplier>().WithMany().HasForeignKey(p => p.SupplierId);
		});
	}
}
