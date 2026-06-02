using Avtosalon.Models.Katalog;
using Avtosalon.Repositories.CarModels;
using TradeFlow.Models.Exceptions;

namespace Avtosalon.Services.CarModels
{
    public class CarModelService : ICarModelService
    {
        private readonly CarModelRepository carModelRepository;
        public CarModelService(CarModelRepository carModelRepository)
        {
            this.carModelRepository = carModelRepository;
        }

        public async ValueTask<CarModel> AddCarModelAsync(CarModel carModel)
        {
            if(carModel is null)
            {
                throw new ValidationException("CarModel null bo'lishi mumkun emas.");
            }

            if(carModel.Id == Guid.Empty)
            {
                throw new ValidationException("CarModel Id bo'sh bo'lishi mumkun emas.");
            }

            if(string.IsNullOrWhiteSpace(carModel.Name))
            {
                throw new ValidationException("CarModel name bo'sh yoki null bo'lishi mumkun emas.");
            }

            CarModel addedCarModel = await this.carModelRepository.InsertCarModelAsync(carModel);
            
            if(addedCarModel is null)
            {
                throw new ValidationException("CarModel qo'shishda xatolik yuz berdi.");
            }

            return addedCarModel;
        }

        public IQueryable<CarModel> RetrieveAllCarModels() =>
            this.carModelRepository.SelectAllCarModels();

        public async ValueTask<CarModel> RetrieveCarModelByIdAsync(Guid carModelId)
        {
            if(carModelId == Guid.Empty)
            {
                throw new ValidationException("CarModelId bo'sh bo'lishi mumkin emas");
            }

            CarModel maybeCarModel = await this.carModelRepository.SelectCarModelByIdAsync(carModelId);

            if(maybeCarModel is null)
            {
                throw new NotFoundException($"{carModelId} id bilan CarModel topilmadi");
            }

            return maybeCarModel;
        }

        public async ValueTask<CarModel> ModifyCarModelAsync(CarModel carModel)
        {
            if(carModel is null)
            {
                throw new ValidationException("CarModel null bo'lishi mumkun emas");
            }

            if(carModel.Id == Guid.Empty)
            {
                throw new ValidationException("CarModel id bo'sh bo'lishi mumkun emas");
            }

            if(string.IsNullOrWhiteSpace(carModel.Name))
            {
                throw new ValidationException("CarModel name bo'sh yoki null bo'lishi mumkun emas");
            }

            CarModel modifiedCarModel = await this.carModelRepository.UpdateCarModelAsync(carModel);

            if(modifiedCarModel is null)
            {
                throw new NotFoundException($"{carModel.Id} id bilan CarModel topilmadi");
            }

            return modifiedCarModel;
        }

        public async ValueTask<CarModel> RemoveCarModelAsync(Guid carModelId)
        {
            if(carModelId == Guid.Empty)
            {
                throw new ValidationException("CarModelId bo'sh bo'lishi mumkun emas.");
            }

            CarModel maybeCarModel = await this.carModelRepository.SelectCarModelByIdAsync(carModelId);

            if(maybeCarModel is null)
            {
                throw new NotFoundException($"{carModelId} id bilan CarModel topilmadi");
            }

            return await this.carModelRepository.DeleteCarModelAsync(maybeCarModel);
        }
    }
}
