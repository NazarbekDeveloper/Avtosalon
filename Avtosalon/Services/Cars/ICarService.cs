using Avtosalon.Models.Cars;

namespace Avtosalon.Services.Cars
{
    public interface ICarService
    {
        ValueTask<Car> AddCarAsync(Car car);
        IQueryable<Car> RetrieveAllCars();
        ValueTask<Car> RetrieveCarByIdAsync(Guid carId);
        ValueTask<Car> ModifyCarAsync(Car car);
        ValueTask<Car> RemoveCarAsync(Guid carId);
    }
}
