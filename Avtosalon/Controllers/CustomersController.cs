using Avtosalon.Models.Persons;
using Avtosalon.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            Customer maybeCustomer = await customerService.AddCustomerAsync(customer);

            return StatusCode(201, maybeCustomer);
        }

        [HttpGet]
        public ActionResult<IQueryable<Customer>> GetAllCustomers()
        {
            IQueryable<Customer> customers = this.customerService.RetrieveAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerByIdAsync(Guid customerId)
        {
            Customer maybeCustomer = await customerService.RetrieveCustomerByIdAsync(customerId);

            return Ok(maybeCustomer);
        }

        [HttpPut]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer customer)
        {
            Customer updatedCustomer = await customerService.ModifyCustomerAsync(customer);

            return Ok(updatedCustomer);
        }

        [HttpDelete("{customerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteCustomerByIdAsync(Guid customerId)
        {
            Customer deletedCustomer = await customerService.RemoveCustomerByIdAsync(customerId);

            return Ok(deletedCustomer);
        }
    }
}
