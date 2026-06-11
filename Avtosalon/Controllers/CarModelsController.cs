using Avtosalon.Models.Katalog;
using Avtosalon.Services.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<CarModel>> PostCarModelAsync(CarModel carModel)
        {
            CarModel addedCarModel = await carModelService.AddCarModelAsync(carModel);

            return StatusCode(201, addedCarModel);
        }

        [HttpGet]
        [Authorize(Roles = "Director, Manager, Seller")]
        public ActionResult<IQueryable<CarModel>> GetAllCarModels()
        {
            IQueryable<CarModel> carModels = this.carModelService.RetrieveAllCarModels();

            return Ok(carModels);
        }

        [HttpGet("{carModelId}")]
        [Authorize(Roles = "Director, Manager, Seller")]
        public async ValueTask<ActionResult<CarModel>> GetCarModelByIdAsync(Guid carModelId)
        {
            CarModel maybeCarModel = await this.carModelService.RetrieveCarModelByIdAsync(carModelId);

            return Ok(maybeCarModel);
        }

        [HttpPut]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<CarModel>> PutCarModelAsync(CarModel carModel)
        {
            CarModel maybeCarModel = await this.carModelService.ModifyCarModelAsync(carModel);

            return Ok(maybeCarModel);
        }

        [HttpDelete("{carModelId}")]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<CarModel>> DeleteCarModelByIdAsync(Guid carModelId)
        {
            CarModel deletedCarModel = await this.carModelService.RemoveCarModelAsync(carModelId);

            return Ok(deletedCarModel);
        }
    }
}
