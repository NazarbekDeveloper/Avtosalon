using Avtosalon.Models.Sales;

namespace Avtosalon.Models.Persons
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string PassportNumber { get; set; }    

        public ICollection<Sale> Sales { get; set; }
    }
}
