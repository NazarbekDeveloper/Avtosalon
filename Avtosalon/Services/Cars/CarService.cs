using Avtosalon.Models.Cars;
using Avtosalon.Repositories.Cars;
using TradeFlow.Models.Exceptions;

namespace Avtosalon.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public async ValueTask<Car> AddCarAsync(Car car)
        {
            if(car is null)
            { 
                throw new ValidationException("Car null bo'lishi mumkin emas.");
            }

            if(car.Name is null)
            {
                throw new ValidationException("Car nomi null bo'lishi mumkin emas.");
            }

            Car myCar = await carRepository.InsertCarAsync(car);

            if(myCar is null)
            {
                throw new Exception("Car qo'shishda xatolik yuz berdi.");
            }

            return myCar;
        }

        public IQueryable<Car> RetrieveAllCars() => 
            carRepository.SelectAllCars();

        public async ValueTask<Car> RetrieveCarByIdAsync(Guid carId)
        {
            if(carId == Guid.Empty)
            {
                throw new ValidationException("Car Id null bo'lishi mumkin emas.");
            }

            Car maybeCar = await carRepository.SelectCarByIdAsync(carId);

            if(maybeCar is null)
            {
                throw new NotFoundException("Car topilmadi.");
            }

            return maybeCar;
        }

        public async ValueTask<Car> ModifyCarAsync(Car car)
        {
            if(car is null)
            {
                throw new ValidationException("Car null bo'lishi mumkin emas.");
            }

            if(car.Name is null || string.IsNullOrWhiteSpace(car.Name))
            {
                throw new ValidationException("Car nomi null yoki bo'sh bo'lishi mumkin emas.");
            }

            if(car.Id == Guid.Empty)
            {
                throw new ValidationException("Car Id null bo'lishi mumkin emas.");
            }

            Car maybeCar = await carRepository.SelectCarByIdAsync(car.Id);

            if(maybeCar is null)
            {
                throw new NotFoundException($"{car.Id} Id-li Car topilmadi.");
            }
            
            return await this.carRepository.UpdateCarAsync(car);
        }

        public async ValueTask<Car> RemoveCarAsync(Car car)
        {
            if(car.Id == Guid.Empty)
            {
                throw new ValidationException("Car Id null bo'lishi mumkin emas.");
            }

            Car maybeCar = await carRepository.SelectCarByIdAsync(car.Id);

            if(maybeCar is null)
            {
                throw new NotFoundException($"{car.Id} Id-li Car topilmadi.");
            }

            return await carRepository.DeleteCarAsync(maybeCar);
        }
    }
}
