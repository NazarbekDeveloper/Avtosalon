using Avtosalon.Models.Sales;

namespace Avtosalon.Models.Persons
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
