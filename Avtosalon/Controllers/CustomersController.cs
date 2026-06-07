using Avtosalon.Models.Persons;
using Avtosalon.Services.Customers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Avtosalon.Models.Exceptions;
using ValidationException = Avtosalon.Models.Exceptions.ValidationException;

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
            try
            {
                Customer maybeCustomer = await customerService.AddCustomerAsync(customer);

                return StatusCode(201,maybeCustomer);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik ro'y berdi.");
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Customer>> GetAllCustomers()
        {
            try
            {
                IQueryable<Customer> customers = this.customerService.RetrieveAllCustomers();

                return Ok(customers);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerByIdAsync(Guid customerId)
        {
            try
            {
                Customer maybeCustomer = await customerService.RetrieveCustomerByIdAsync(customerId);

                return Ok(maybeCustomer);
            }
            catch(Avtosalon.Models.Exceptions.ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik ro'y berdi.");
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer customer)
        {
            try
            {
                Customer updatedCustomer = await customerService.ModifyCustomerAsync(customer);

                return Ok(updatedCustomer);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{customerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteCustomerByIdAsync(Guid customerId)
        {
            try
            {
                Customer deletedCustomer = await customerService.RemoveCustomerByIdAsync(customerId);

                return Ok(deletedCustomer);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik ro'y berdi.");
            }
        }
    }
}
