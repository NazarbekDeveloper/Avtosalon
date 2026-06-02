using Avtosalon.Models.Cars;
using Avtosalon.Models.Katalog;

namespace Avtosalon.Repositories.CarModels
{
    public interface ICarModelRepository
    {
        ValueTask<CarModel> InsertCarModelAsync(CarModel carModel);
        IQueryable<CarModel> SelectAllCarModels();
        ValueTask<CarModel> SelectCarModelByIdAsync(Guid carModelId);
        ValueTask<CarModel> UpdateCarModelAsync(CarModel carModel);
        ValueTask<CarModel> DeleteCarModelAsync(CarModel carModel);
    }
}
