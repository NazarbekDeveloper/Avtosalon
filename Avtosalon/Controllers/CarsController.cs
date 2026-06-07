using Avtosalon.Models.Cars;
using Avtosalon.Services.Cars;
using Microsoft.AspNetCore.Mvc;
using Avtosalon.Models.Exceptions;
using ValidationException = Avtosalon.Models.Exceptions.ValidationException;

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
            try
            {
                Car addedCar = await carService.AddCarAsync(car);

                return StatusCode(201, addedCar);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi");
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Car>> GetAllCars()
        {
            try
            {
                IQueryable<Car> cars = carService.RetrieveAllCars();

                return Ok(cars);
            }
            catch (Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpGet("{carId}")]
        public async ValueTask<ActionResult<Car>> GetCarByIdAsync(Guid carId)
        {
            try
            {
                Car maybeCar = await carService.RetrieveCarByIdAsync(carId);

                return Ok(maybeCar);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Serverda xatolik yuz berdi");
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Car>> PutCarAsync(Car car)
        {
            try
            {
                Car updatedCar = await carService.ModifyCarAsync(car);

                return Ok(updatedCar);
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
                return StatusCode(500, "Serverda xatolik yuz berdi");
            }
        }

        [HttpDelete("{carId}")]
        public async ValueTask<ActionResult<Car>> DeleteCarAsync(Guid carId)
        {
            try
            {
                Car deletedCar = await carService.RemoveCarAsync(carId);

                return Ok(deletedCar);
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
                return StatusCode(500, "Serverda xatolik yuz berdi");
            }
        }
    }
}
