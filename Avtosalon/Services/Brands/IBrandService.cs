using Avtosalon.Models.Katalog;

namespace Avtosalon.Services.Brands
{
    public interface IBrandService
    {
        ValueTask<Brand> AddBrandAsync(Brand brand);
        IQueryable<Brand> RetrieveAllBrands();
        ValueTask<Brand> RetrieveBrandByIdAsync(Guid brandId);
        ValueTask<Brand> ModifyBrandAsync(Brand brand);
        ValueTask<Brand> RemoveBrandAsync(Guid brandId);
    }
}
