using Avtosalon.Models.Persons;
using Avtosalon.Repositories.Employees;
using TradeFlow.Models.Exceptions;

namespace Avtosalon.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async ValueTask<Employee> AddEmployeeAsync(Employee employee)
        {
            if(employee is null)
            {
                throw new ValidationException("Employee null bo'lishi mumkun emas.");
            }

            if(employee.Id == Guid.Empty)
            {
                throw new ValidationException("Employee Id bo'sh bo'lishi mumkun emas.");
            }

            if(string.IsNullOrWhiteSpace(employee.FullName))
            {
                throw new ValidationException("Employee FullName bo'sh bo'lishi mumkun emas.");
            }

            Employee addedEmployee = await this.employeeRepository.InsertEmployeeAsync(employee);

            return addedEmployee;
        }

        public IQueryable<Employee> RetrieveAllEmployees() =>
            this.employeeRepository.SelectAllEmployees();

        public async ValueTask<Employee> RetrieveEmployeeByIdAsync(Guid employeeId)
        {
            if(employeeId == Guid.Empty)
            {
                throw new ValidationException("employeeId bo'sh bo'lishi mumkun emas.");
            }

            Employee maybeEmployee = await this.employeeRepository.SelectEmployeeByIdAsync(employeeId);

            if(maybeEmployee is null)
            {
                throw new NotFoundException($"{employeeId} id bilan Employee topilmadi.");
            }

            return maybeEmployee;
        }

        public async ValueTask<Employee> ModifyEmployeeAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new ValidationException("Employee null bo'lishi mumkun emas.");
            }

            if (employee.Id == Guid.Empty)
            {
                throw new ValidationException("Employee Id bo'sh bo'lishi mumkun emas.");
            }

            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                throw new ValidationException("Employee FullName bo'sh bo'lishi mumkun emas.");
            }

            Employee modifiedEmployee = await this.employeeRepository.UpdateEmployeeAsync(employee);

            if(modifiedEmployee is null)
            {
                throw new NotFoundException($"{employee.Id} id bilan Employee topilmadi");
            }

            return modifiedEmployee;
        }

        public async ValueTask<Employee> RemoveEmployeeByIdAsync(Guid employeeId)
        {
            if(employeeId == Guid.Empty)
            {
                throw new ValidationException("EmployeeId bo'sh bo'lishi mumkun emas");
            }

            Employee maybeEmployee = await this.employeeRepository.SelectEmployeeByIdAsync(employeeId);

            if( maybeEmployee is null)
            {
                throw new NotFoundException($"{employeeId} id bilan Employee topilmadi.");
            }

            return await this.employeeRepository.DeleteEmployeeAsync(maybeEmployee);
        }
    }
}
