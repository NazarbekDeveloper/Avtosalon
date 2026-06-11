using Avtosalon.Models.Persons;
using Avtosalon.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Director")]
        public async ValueTask<ActionResult<Employee>> PostEmployeeAsync(Employee employee)
        {
            Employee addedEmployee = await this.employeeService.AddEmployeeAsync(employee);

            return StatusCode(201, addedEmployee);
        }

        [HttpGet]
        [Authorize(Roles = "Director, Manager")]
        public ActionResult<IQueryable<Employee>> GetAllEmployees()
        {
            IQueryable<Employee> employees = this.employeeService.RetrieveAllEmployees();

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Employee>> GetEmployeeByIdAsync(Guid employeeId)
        {
            Employee maybeEmployee = await this.employeeService.RetrieveEmployeeByIdAsync(employeeId);

            return Ok(maybeEmployee);
        }

        [HttpPut]
        [Authorize(Roles = "Director")]
        public async ValueTask<ActionResult<Employee>> PutEmployeeAsync(Employee employee)
        {
            Employee editedEmployee = await this.employeeService.ModifyEmployeeAsync(employee);

            return Ok(editedEmployee);
        }

        [HttpDelete("{employeeId}")]
        [Authorize(Roles = "Director")]
        public async ValueTask<ActionResult<Employee>> DeleteEmployeeByIdAsync(Guid employeeId)
        {
            Employee deletedEmployee = await this.employeeService.RemoveEmployeeByIdAsync(employeeId);

            return Ok(deletedEmployee);
        }
    }
}
