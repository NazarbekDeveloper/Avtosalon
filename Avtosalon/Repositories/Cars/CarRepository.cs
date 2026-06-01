using Avtosalon.Data;
using Avtosalon.Models.Cars;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Cars
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CarRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Car> InsertCarAsync(Car car)
        {
            await applicationDbContext.Cars.AddAsync(car);
            await applicationDbContext.SaveChangesAsync();

            return car;
        }

        public IQueryable<Car> SelectAllCars() =>
            this.applicationDbContext.Cars;
        
        public async ValueTask<Car> SelectCarByIdAsync(Guid carId) =>
            await this.applicationDbContext.Cars.FindAsync(carId);

        public async ValueTask<Car> UpdateCarAsync(Car car)
        {
            this.applicationDbContext.Entry(car).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return car;
        }

        public async ValueTask<Car> DeleteCarAsync(Car car)
        {
            this.applicationDbContext.Entry(car).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return car;
        }
    }
}
