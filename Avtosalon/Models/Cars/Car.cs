using Avtosalon.Models.Katalog;
using Avtosalon.Models.Sales;

namespace Avtosalon.Models.Cars
{
    public class Car
    {
        public Guid Id { get; set; }
        public Guid CarModelId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string VIN { get; set; }

        public CarModel CarModel { get; set; }
        public Sale Sale { get; set; }
    }
}
