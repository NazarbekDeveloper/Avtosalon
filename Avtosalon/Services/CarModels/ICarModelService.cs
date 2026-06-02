using Avtosalon.Models.Cars;
using Avtosalon.Models.Katalog;

namespace Avtosalon.Services.CarModels
{
    public interface ICarModelService
    {
        ValueTask<CarModel> AddCarModelAsync(CarModel carModel);
        IQueryable<CarModel> RetrieveAllCarModels();
        ValueTask<CarModel> RetrieveCarModelByIdAsync(Guid carModelId);
        ValueTask<CarModel> ModifyCarModelAsync(CarModel carModel);
        ValueTask<CarModel> RemoveCarModelAsync(Guid carModelId);
    }
}
