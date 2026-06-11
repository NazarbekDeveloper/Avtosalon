using Avtosalon.Models.Cars;
using Avtosalon.Models.Katalog;
using Avtosalon.Models.Persons;
using Avtosalon.Models.Sales;
using Avtosalon.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(cM => cM.CarModels)
                .WithOne(b => b.Brand)
                .HasForeignKey(cM => cM.BrandId);

            modelBuilder.Entity<CarModel>()
                .HasMany(c => c.Cars)
                .WithOne(cM => cM.CarModel)
                .HasForeignKey(c => c.CarModelId);

            modelBuilder.Entity<Customer>()
                .HasMany(s => s.Sales)
                .WithOne(c => c.Customer)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Employee>()
                .HasMany(s => s.Sales)
                .WithOne(e => e.Employee)
                .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<Car>()
                .HasOne(s => s.Sale)
                .WithOne(c => c.Car)
                .HasForeignKey<Sale>(s => s.CarId);

            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
