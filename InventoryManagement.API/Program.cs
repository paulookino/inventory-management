using InventoryManagement.Application.Mocks;
using InventoryManagement.Application.Services;
using InventoryManagement.Application.Services.Interfaces;
using InventoryManagement.Domain.Interfaces.Repositories;
using InventoryManagement.Infrasctructure.Repositories;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDbContext>(options =>
	options.UseInMemoryDatabase("AppDb"));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddSingleton<IWmsClient, WmsClientMock>();
builder.Services.AddSingleton<IAuditClient, AuditClientMock>();
builder.Services.AddSingleton<IEmailSender, EmailSenderService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
