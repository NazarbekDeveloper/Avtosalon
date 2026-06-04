using Avtosalon.Models.Persons;

namespace Avtosalon.Repositories.Customers
{
    public interface ICustomerRepository
    {
        ValueTask<Customer> InsertCustomerAsync(Customer customer);
        IQueryable<Customer> SelectAllCustomers();
        ValueTask<Customer> SelectCustomerById(Guid customerId);
        ValueTask<Customer> UpdateCustomerAsync(Customer customer);
        ValueTask<Customer> DeleteCustomerAsync(Customer customer);
    }
}
