using Avtosalon.Models.Cars;
using Avtosalon.Services.Cars;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService carService;
        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Car>> PostCarAsync(Car car)
        {
            Car addedCar = await carService.AddCarAsync(car);

            return StatusCode(201, addedCar);
        }

        [HttpGet]
        public ActionResult<IQueryable<Car>> GetAllCars()
        {
            IQueryable<Car> cars = carService.RetrieveAllCars();

            return Ok(cars);
        }

        [HttpGet("{carId}")]
        public async ValueTask<ActionResult<Car>> GetCarByIdAsync(Guid carId)
        {
            Car maybeCar = await carService.RetrieveCarByIdAsync(carId);

            return Ok(maybeCar);
        }

        [HttpPut]
        public async ValueTask<ActionResult<Car>> PutCarAsync(Car car)
        {
            Car updatedCar = await carService.ModifyCarAsync(car);

            return Ok(updatedCar);
        }

        [HttpDelete("{carId}")]
        public async ValueTask<ActionResult<Car>> DeleteCarAsync(Guid carId)
        {
            Car deletedCar = await carService.RemoveCarAsync(carId);

            return Ok(deletedCar);
        }
    }
}
