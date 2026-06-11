using Avtosalon.Models.Katalog;
using Avtosalon.Services.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Brand>> PostBrandAsync(Brand brand)
        {
            Brand addedBrand = await brandService.AddBrandAsync(brand);

            return StatusCode(201, addedBrand);
        }

        [HttpGet]
        [Authorize(Roles = "Director, Manager, Seller")]
        public ActionResult<IQueryable<Brand>> GetAllBrand()
        {
            IQueryable<Brand> brands = brandService.RetrieveAllBrands();

            return StatusCode(200, brands);
        }

        [HttpGet("{brandId}")]
        [Authorize(Roles = "Director, Manager, Seller")]
        public async ValueTask<ActionResult<Brand>> GetBrandById(Guid brandId)
        {
            Brand maybeBrand = await brandService.RetrieveBrandByIdAsync(brandId);

            return Ok(maybeBrand);
        }

        [HttpPut]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Brand>> PutBrandAsync(Brand brand)
        {
            Brand maybeBrand = await brandService.ModifyBrandAsync(brand);

            return Ok(maybeBrand);
        }

        [HttpDelete("{brandId}")]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Brand>> DeleteBrandAsync(Guid brandId)
        {
            Brand deletedBrand = await brandService.RemoveBrandAsync(brandId);

            return Ok(deletedBrand);
        }
    }
}
