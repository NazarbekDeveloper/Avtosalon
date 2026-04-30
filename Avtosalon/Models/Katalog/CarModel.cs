using Avtosalon.Models.Cars;

namespace Avtosalon.Models.Katalog
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Brand Brand { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
