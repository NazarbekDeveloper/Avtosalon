using Avtosalon.Models.Cars;
using Avtosalon.Models.Persons;

namespace Avtosalon.Models.Sales
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }

    public enum PaymentType
    {
        Cash,
        Credit,
        Transfer
    }
}
