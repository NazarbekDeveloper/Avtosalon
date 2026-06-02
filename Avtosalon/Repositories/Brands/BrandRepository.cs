using Avtosalon.Data;
using Avtosalon.Models.Katalog;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Brands
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public BrandRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Brand> InsertBrandAsync(Brand brand)
        {
            await this.applicationDbContext.AddAsync(brand);
            await this.applicationDbContext.SaveChangesAsync();

            return brand;
        }

        public IQueryable<Brand> SelectAllBrands() =>
            this.applicationDbContext.Brands;

        public async ValueTask<Brand> SelectBrandByIdAsync(Guid brandId) =>
            await this.applicationDbContext.Brands.FindAsync(brandId);

        public async ValueTask<Brand> UpdateBrandAsync(Brand brand)
        {
            this.applicationDbContext.Brands.Entry(brand).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return brand;
        }
        public async ValueTask<Brand> DeleteBrandAsync(Brand brand)
        {
            this.applicationDbContext.Brands.Entry(brand).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return brand;
        }
    }
}
