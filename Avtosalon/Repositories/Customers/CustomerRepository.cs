using Avtosalon.Data;
using Avtosalon.Models.Persons;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Customer> InsertCustomerAsync(Customer customer)
        {
            await this.applicationDbContext.Customers.AddAsync(customer);
            await this.applicationDbContext.SaveChangesAsync();

            return customer;
        }

        public IQueryable<Customer> SelectAllCustomers() =>
            this.applicationDbContext.Customers;

        public async ValueTask<Customer> SelectCustomerById(Guid customerId) =>
            await this.applicationDbContext.Customers.FindAsync(customerId);

        public async ValueTask<Customer> UpdateCustomerAsync(Customer customer)
        {
            this.applicationDbContext.Entry(customer).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return customer;
        }

        public async ValueTask<Customer> DeleteCustomerAsync(Customer customer)
        {
            this.applicationDbContext.Entry(customer).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return customer;
        }
    }
}
