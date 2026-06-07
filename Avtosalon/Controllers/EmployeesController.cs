using Avtosalon.Models.Persons;
using Avtosalon.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using Avtosalon.Models.Exceptions;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Employee>> PostEmployeeAsync(Employee employee)
        {
            try
            {
                Employee addedEmployee = await this.employeeService.AddEmployeeAsync(employee);

                return StatusCode(201, addedEmployee);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik yuz berdi.");
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Employee>> GetAllEmployees()
        {
            try
            {
                IQueryable<Employee> employees = this.employeeService.RetrieveAllEmployees();

                return Ok(employees);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpGet("{employeeId}")]
        public async ValueTask<ActionResult<Employee>> GetEmployeeByIdAsync(Guid employeeId)
        {
            try
            {
                Employee maybeEmployee = await this.employeeService.RetrieveEmployeeByIdAsync(employeeId);

                return Ok(maybeEmployee);
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
                return BadRequest("Serverda xatolik yuz berdi.");
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Employee>> PutEmployeeAsync(Employee employee)
        {
            try
            {
                Employee editedEmployee = await this.employeeService.ModifyEmployeeAsync(employee);

                return Ok(editedEmployee);
            }
            catch(ValidationException validatoinException)
            {
                return BadRequest(validatoinException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik yuz berdi.");
            }
        }

        [HttpDelete("{employeeId}")]
        public async ValueTask<ActionResult<Employee>> DeleteEmployeeByIdAsync(Guid employeeId)
        {
            try
            {
                Employee deletedEmployee = await this.employeeService.RemoveEmployeeByIdAsync(employeeId);

                return Ok(deletedEmployee);
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
                return BadRequest("Serverda xatolik yuz berdi.");
            }
        }
    }
}
