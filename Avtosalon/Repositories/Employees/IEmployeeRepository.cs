using Avtosalon.Models.Persons;

namespace Avtosalon.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        ValueTask<Employee> InsertEmployeeAsync(Employee Employee);
        IQueryable<Employee> SelectAllEmployees();
        ValueTask<Employee> SelectEmployeeByIdAsync(Guid employeeId);
        ValueTask<Employee> UpdateEmployeeAsync(Employee employee);
        ValueTask<Employee> DeleteEmployeeAsync(Employee employee);
    }
}
