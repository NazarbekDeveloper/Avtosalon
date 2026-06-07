using Avtosalon.Data;
using Avtosalon.Models.Persons;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Employee> InsertEmployeeAsync(Employee Employee)
        {
            await this.applicationDbContext.Employees.AddAsync(Employee);
            await this.applicationDbContext.SaveChangesAsync();

            return Employee;
        }

        public IQueryable<Employee> SelectAllEmployees() =>
            this.applicationDbContext.Employees;

        public async ValueTask<Employee> SelectEmployeeByIdAsync(Guid employeeId) =>
            await this.applicationDbContext.Employees.FindAsync(employeeId);

        public async ValueTask<Employee> UpdateEmployeeAsync(Employee employee)
        {
            this.applicationDbContext.Employees.Entry(employee).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return employee;    
        }

        public async ValueTask<Employee> DeleteEmployeeAsync(Employee employee)
        {
            this.applicationDbContext.Employees.Entry(employee).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return employee;
        }
    }
}
