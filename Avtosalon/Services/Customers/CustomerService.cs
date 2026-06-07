using Avtosalon.Models.Katalog;
using Avtosalon.Models.Persons;
using Avtosalon.Repositories.Customers;
using Avtosalon.Models.Exceptions;

namespace Avtosalon.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async ValueTask<Customer> AddCustomerAsync(Customer customer)
        {
            if(customer is null)
            {
                throw new ValidationException("Customer null bo'lishi mumkin emas.");
            }
          
            if(string.IsNullOrWhiteSpace(customer.FullName))
            {
                throw new ValidationException("Customer FullName bo'sh bo'lishi mumkun emas.");
            }

            customer.Id = Guid.NewGuid();

            return await customerRepository.InsertCustomerAsync(customer);
        }

        public IQueryable<Customer> RetrieveAllCustomers() =>
            this.customerRepository.SelectAllCustomers();

        public async ValueTask<Customer> RetrieveCustomerByIdAsync(Guid customerId)
        {
            if(customerId == Guid.Empty)
            {
                throw new ValidationException("CustomerId bo'sh bo'lishi mumkun emas.");
            }

            Customer maybeCustomer = await customerRepository.SelectCustomerById(customerId);

            if(maybeCustomer is null)
            {
                throw new NotFoundException($"{customerId} id bilan Customer topilmadi.");
            }

            return maybeCustomer;
        }

        public async ValueTask<Customer> ModifyCustomerAsync(Customer customer)
        {
            if(customer is null)
            {
                throw new ValidationException("Customer null bo'lishi mumkun emas.");
            }

            if(customer.Id == Guid.Empty)
            {
                throw new ValidationException("CustomerId bo'sh bo'lishi mumkin emas.");
            }

            if(string.IsNullOrWhiteSpace(customer.FullName))
            {
                throw new ValidationException("Customer FullName bo'sh bo'lishi mumkin emas.");
            }

            Customer modifiedCustomer = await customerRepository.UpdateCustomerAsync(customer);

            if(modifiedCustomer is null)
            {
                throw new NotFoundException($"{customer.Id} id bilan Customer topilmadi.");
            }

            return modifiedCustomer;
        }

        public async ValueTask<Customer> RemoveCustomerByIdAsync(Guid customerId)
        {
            if(customerId == Guid.Empty)
            {
                throw new ValidationException("CustomerId bo'sh bo'lishi mumkun emas.");
            }

            Customer maybeCustomer = await customerRepository.SelectCustomerById(customerId);

            if( maybeCustomer is null)
            {
                throw new NotFoundException($"{customerId} id bilan Customer topilmadi.");
            }

            return await customerRepository.DeleteCustomerAsync(maybeCustomer);
        }
    }
}
