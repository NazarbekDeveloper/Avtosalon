using Avtosalon.Models.Katalog;
using Avtosalon.Services.CarModels;
using Microsoft.AspNetCore.Mvc;
using Avtosalon.Models.Exceptions;
using ValidationException = Avtosalon.Models.Exceptions.ValidationException;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarModelsController : ControllerBase
    {
        private readonly ICarModelService carModelService;
        public CarModelsController(ICarModelService carModelService)
        {
            this.carModelService = carModelService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<CarModel>> PostCarModelAsync(CarModel carModel)
        {
            try
            {
                CarModel addedCarModel = await carModelService.AddCarModelAsync(carModel);

                return StatusCode(201,addedCarModel);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<CarModel>> GetAllCarModels()
        {
            try
            {
                IQueryable<CarModel> carModels = this.carModelService.RetrieveAllCarModels();

                return Ok(carModels);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{carModelId}")]
        public async ValueTask<ActionResult<CarModel>> GetCarModelByIdAsync(Guid carModelId)
        {
            try
            {
                CarModel maybeCarModel = await this.carModelService.RetrieveCarModelByIdAsync(carModelId);

                return Ok(maybeCarModel);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<CarModel>> PutCarModelAsync(CarModel carModel)
        {
            try
            {
                CarModel maybeCarModel = await this.carModelService.ModifyCarModelAsync(carModel);

                return Ok(maybeCarModel);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{carModelId}")]
        public async ValueTask<ActionResult<CarModel>> DeleteCarModelByIdAsync(Guid carModelId)
        {
            try
            {
                CarModel deletedCarModel = await this.carModelService.RemoveCarModelAsync(carModelId);

                return Ok(deletedCarModel);
            }
            catch(ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
