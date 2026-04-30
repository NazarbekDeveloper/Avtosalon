namespace Avtosalon.Models.Katalog
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
