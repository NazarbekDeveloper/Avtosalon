using Avtosalon.Models.Cars;
using Avtosalon.Services.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Car>> PostCarAsync(Car car)
        {
            Car addedCar = await carService.AddCarAsync(car);

            return StatusCode(201, addedCar);
        }

        [HttpGet]
        [Authorize(Roles = "Director, Manager, Seller")]
        public ActionResult<IQueryable<Car>> GetAllCars()
        {
            IQueryable<Car> cars = carService.RetrieveAllCars();

            return Ok(cars);
        }

        [HttpGet("{carId}")]
        [Authorize(Roles = "Director, Manager, Seller")]
        public async ValueTask<ActionResult<Car>> GetCarByIdAsync(Guid carId)
        {
            Car maybeCar = await carService.RetrieveCarByIdAsync(carId);

            return Ok(maybeCar);
        }

        [HttpPut]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Car>> PutCarAsync(Car car)
        {
            Car updatedCar = await carService.ModifyCarAsync(car);

            return Ok(updatedCar);
        }

        [HttpDelete("{carId}")]
        [Authorize(Roles = "Director, Manager")]
        public async ValueTask<ActionResult<Car>> DeleteCarAsync(Guid carId)
        {
            Car deletedCar = await carService.RemoveCarAsync(carId);

            return Ok(deletedCar);
        }
    }
}
