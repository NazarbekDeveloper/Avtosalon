using Avtosalon.Models.Katalog;
using Avtosalon.Repositories.Brands;
using TradeFlow.Models.Exceptions;

namespace Avtosalon.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async ValueTask<Brand> AddBrandAsync(Brand brand)
        {
            if(brand is null)
            {
                throw new ValidationException("Brand null bo'lishi mumkun emas.");
            }

            if(brand.Name is null || string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ValidationException("Brand name null yoki bo'sh bo'lishi mumkin emas.");
            }

            if(brand.Id == Guid.Empty)
            {
                throw new ValidationException("Brand id bo'sh bo'lishi mumkun emas");
            }

            Brand addedBrand = await this.brandRepository.InsertBrandAsync(brand);

            if(addedBrand is null)
            {
                throw new ValidationException("Brand qo'shishda xatolik yuz berdi");
            }

            return addedBrand;
        }

        public IQueryable<Brand> RetrieveAllBrands() =>
            this.brandRepository.SelectAllBrands();

        public async ValueTask<Brand> RetrieveBrandByIdAsync(Guid brandId)
        {
            if(brandId == Guid.Empty)
            {
                throw new ValidationException("Brand id bo'sh bo'lishi mumkun emas");
            }

            Brand maybeBrand = await this.brandRepository.SelectBrandByIdAsync(brandId);    

            if(maybeBrand is null)
            {
                throw new NotFoundException($"{brandId} id bilan brand topilmadi");
            }

            return maybeBrand;
        }

        public async ValueTask<Brand> ModifyBrandAsync(Brand brand)
        {
            if(brand is null)
            {
                throw new ValidationException("Brand null bo'lishi mumkun emas.");
            }

            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ValidationException("Brand name null yoki bo'sh bo'lishi mumkun emas");
            }

            if(brand.Id == Guid.Empty)
            {
                throw new ValidationException("Brand id bo'sh bo'lishi mumkun emas");
            }
            
            Brand modifiedBrand = await this.brandRepository.UpdateBrandAsync(brand);

            if(modifiedBrand is null)
            {
                throw new ValidationException("Brand yangilashda qandaydir xatolik yuz berdi");
            }

            return modifiedBrand;
        }

        public async ValueTask<Brand> RemoveBrandAsync(Guid brandId)
        {
            if(brandId == Guid.Empty)
            {
                throw new ValidationException("BrandId null bo'lishi mumkun emas.");
            }

            Brand maybeBrand = await brandRepository.SelectBrandByIdAsync(brandId);

            if(maybeBrand is null)
            {
                throw new NotFoundException($"{brandId} id bilan brand topilmadi");
            }

            return await brandRepository.DeleteBrandAsync(maybeBrand);
        }
    }
}
