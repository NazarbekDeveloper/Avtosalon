using Avtosalon.Models.Katalog;
using Avtosalon.Services.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService brandService;
        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Brand>> PostBrandAsync(Brand brand)
        {
            Brand addedBrand = await brandService.AddBrandAsync(brand);

            return StatusCode(201, addedBrand);
        }

        [HttpGet]
        public ActionResult<IQueryable<Brand>> GetAllBrand()
        {
            IQueryable<Brand> brands = brandService.RetrieveAllBrands();

            return StatusCode(200, brands);
        }

        [HttpGet("{brandId}")]
        public async ValueTask<ActionResult<Brand>> GetBrandById(Guid brandId)
        {
            Brand maybeBrand = await brandService.RetrieveBrandByIdAsync(brandId);

            return Ok(maybeBrand);
        }

        [HttpPut]
        public async ValueTask<ActionResult<Brand>> PutBrandAsync(Brand brand)
        {
            Brand maybeBrand = await brandService.ModifyBrandAsync(brand);

            return Ok(maybeBrand);
        }

        [HttpDelete("{brandId}")]
        public async ValueTask<ActionResult<Brand>> DeleteBrandAsync(Guid brandId)
        {
            Brand deletedBrand = await brandService.RemoveBrandAsync(brandId);

            return Ok(deletedBrand);
        }
    }
}
