using Avtosalon.Data;
using Avtosalon.Models.Katalog;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.CarModels
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CarModelRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<CarModel> InsertCarModelAsync(CarModel carModel)
        {
            await this.applicationDbContext.CarModels.AddAsync(carModel);
            this.applicationDbContext.SaveChangesAsync();

            return carModel;
        }

        public IQueryable<CarModel> SelectAllCarModels() =>
            this.applicationDbContext.CarModels;

        public async ValueTask<CarModel> SelectCarModelByIdAsync(Guid carModelId) => 
            await this.applicationDbContext.CarModels.FindAsync(carModelId);
                   
        public async ValueTask<CarModel> UpdateCarModelAsync(CarModel carModel)
        {
            this.applicationDbContext.CarModels.Entry(carModel).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return carModel;
        }

        public async ValueTask<CarModel> DeleteCarModelAsync(CarModel carModel)
        {
            this.applicationDbContext.CarModels.Entry(carModel).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return carModel;
        }
    }
}
