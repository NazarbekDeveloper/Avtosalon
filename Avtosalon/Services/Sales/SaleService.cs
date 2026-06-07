using Avtosalon.Models.Cars;
using Avtosalon.Models.Exceptions;
using Avtosalon.Models.Persons;
using Avtosalon.Models.Sales;
using Avtosalon.Repositories.Cars;
using Avtosalon.Repositories.Customers;
using Avtosalon.Repositories.Employees;
using Avtosalon.Repositories.Sales;

namespace Avtosalon.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository saleRepository;
        private readonly ICarRepository carRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IEmployeeRepository employeeRepository;

        public SaleService(
            ISaleRepository saleRepository,
            ICarRepository carRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository
            )
        {
            this.saleRepository = saleRepository;
            this.carRepository = carRepository;
            this.customerRepository = customerRepository;
            this.employeeRepository = employeeRepository;
        }

        public async ValueTask<Sale> AddSaleAsync(Sale sale)
        {
            if (sale is null)
                throw new ValidationException("Sale null bo'lishi mumkin emas.");

            Car maybeCar = await this.carRepository.SelectCarByIdAsync(sale.CarId);
            if (maybeCar is null)
                throw new NotFoundException($"{sale.CarId} Id-li car topilmadi.");

            Customer maybeCustomer = await this.customerRepository.SelectCustomerById(sale.CustomerId);
            if (maybeCustomer is null)
                throw new NotFoundException($"{sale.CustomerId} Id-li Customer topilmadi.");

            Employee maybeEmployee = await this.employeeRepository.SelectEmployeeByIdAsync(sale.EmployeeId);
            if (maybeEmployee is null)
                throw new NotFoundException($"{sale.EmployeeId} Id-li Employee topilmadi.");

            sale.Id = Guid.NewGuid();
            sale.SaleDate = DateTime.UtcNow;
            sale.TotalPrice = maybeCar.Price;

            return await saleRepository.InsertSaleAsync(sale);
        }

        public IQueryable<Sale> RetrieveAllSales() =>
            this.saleRepository.SelectAllSales();

        public async ValueTask<Sale> RetrieveSaleByIdAsync(Guid saleId)
        {
            if (saleId == Guid.Empty)
                throw new ValidationException("SaleId bo'sh bo'lishi mumkun emas.");

            Sale maybeSale = await this.saleRepository.SelectSaleByIdAsync(saleId);

            if (maybeSale is null)
                throw new NotFoundException($"{saleId} Id-li Sale topilmadi");

            return maybeSale;
        }

        public async ValueTask<Sale> ModifySaleAsync(Sale sale)
        {
            if (sale is null)
                throw new ValidationException("Sale null bo'lishi mumkin emas.");

            if (sale.Id == Guid.Empty)
                throw new ValidationException("SaleId bo'sh bo'lishi mumkin emas.");

            var maybeSale = await saleRepository.SelectSaleByIdAsync(sale.Id);
            if (maybeSale is null)
                throw new NotFoundException($"{sale.Id} Id-li Sale topilmadi.");

            return await saleRepository.UpdateSaleAsync(sale);
        }

        public async ValueTask<Sale> RemoveSaleAsync(Guid saleId)
        {
            if (saleId == Guid.Empty)
                throw new ValidationException("SaleId bo'sh bo'lishi mumkin emas.");

            var maybeSale = await saleRepository.SelectSaleByIdAsync(saleId);
            if (maybeSale is null)
                throw new NotFoundException($"{saleId} Id-li Sale topilmadi.");

            return await saleRepository.DeleteSaleAsync(maybeSale);
        }
    }
}
