using Avtosalon.Data;
using Avtosalon.Repositories.Brands;
using Avtosalon.Repositories.CarModels;
using Avtosalon.Repositories.Cars;
using Avtosalon.Repositories.Customers;
using Avtosalon.Repositories.Employees;
using Avtosalon.Services.Brands;
using Avtosalon.Services.CarModels;
using Avtosalon.Services.Cars;
using Avtosalon.Services.Customers;
using Avtosalon.Services.Employees;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<ICarModelRepository, CarModelRepository>();
builder.Services.AddTransient<ICarModelService, CarModelService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();