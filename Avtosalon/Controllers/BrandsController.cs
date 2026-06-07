using Avtosalon.Models.Katalog;
using Avtosalon.Services.Brands;
using Microsoft.AspNetCore.Mvc;
using Avtosalon.Models.Exceptions;
using ValidationException = Avtosalon.Models.Exceptions.ValidationException;

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
            try
            {
                Brand addedBrand = await brandService.AddBrandAsync(brand);

                return StatusCode(201, addedBrand);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest("Serverda xatolik yuz berdi.");
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Brand>> GetAllBrand()
        {
            try
            {
                IQueryable<Brand> brands = brandService.RetrieveAllBrands();

                return StatusCode(200,brands);
            }
            catch(Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpGet("{brandId}")]
        public async ValueTask<ActionResult<Brand>> GetBrandById(Guid brandId)
        {
            try
            {
                Brand maybeBrand = await brandService.RetrieveBrandByIdAsync(brandId);

                return Ok(maybeBrand);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException  notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Brand>> PutBrandAsync(Brand brand)
        {
            try
            {
                Brand maybeBrand = await brandService.ModifyBrandAsync(brand);

                return Ok(maybeBrand);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpDelete("{brandId}")]
        public async ValueTask<ActionResult<Brand>> DeleteBrandAsync(Guid brandId)
        {
            try
            {
                Brand deletedBrand = await brandService.RemoveBrandAsync(brandId);

                return Ok(deletedBrand);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }
    }
}
