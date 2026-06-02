using Avtosalon.Models.Katalog;

namespace Avtosalon.Repositories.Brands
{
    public interface IBrandRepository
    {
        ValueTask<Brand> InsertBrandAsync(Brand brand);
        IQueryable<Brand> SelectAllBrands();
        ValueTask<Brand> SelectBrandByIdAsync(Guid brandId);
        ValueTask<Brand> UpdateBrandAsync(Brand brand);
        ValueTask<Brand> DeleteBrandAsync(Brand brand);
    }
}
