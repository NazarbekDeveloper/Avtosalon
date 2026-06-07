using Avtosalon.Models.Persons;

namespace Avtosalon.Services.Employees
{
    public interface IEmployeeService
    {
        ValueTask<Employee> AddEmployeeAsync(Employee employee);
        IQueryable<Employee> RetrieveAllEmployees();
        ValueTask<Employee> RetrieveEmployeeByIdAsync(Guid employeeId);
        ValueTask<Employee> ModifyEmployeeAsync(Employee employee);
        ValueTask<Employee> RemoveEmployeeByIdAsync(Guid employeeId);
    }
}
