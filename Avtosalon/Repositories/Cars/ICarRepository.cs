using Avtosalon.Models.Cars;

namespace Avtosalon.Repositories.Cars
{
    public interface ICarRepository
    {
        ValueTask<Car> InsertCarAsync(Car car);
        IQueryable<Car> SelectAllCars();
        ValueTask<Car> SelectCarByIdAsync(Guid carId);
        ValueTask<Car> UpdateCarAsync(Car car);
        ValueTask<Car> DeleteCarAsync(Car car);
    }
}
