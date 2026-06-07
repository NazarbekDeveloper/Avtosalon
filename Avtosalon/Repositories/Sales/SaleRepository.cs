using Avtosalon.Data;
using Avtosalon.Models.Sales;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Sales
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public SaleRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Sale> InsertSaleAsync(Sale sale)
        {
            await this.applicationDbContext.Sales.AddAsync(sale);
            await this.applicationDbContext.SaveChangesAsync();

            return sale;
        }

        public IQueryable<Sale> SelectAllSales() =>
            this.applicationDbContext.Sales;

        public async ValueTask<Sale> SelectSaleByIdAsync(Guid saleId) =>
            await this.applicationDbContext.Sales.FindAsync(saleId);

        public async ValueTask<Sale> UpdateSaleAsync(Sale sale)
        {
            this.applicationDbContext.Sales.Entry(sale).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return sale;
        }

        public async ValueTask<Sale> DeleteSaleAsync(Sale sale)
        {
            this.applicationDbContext.Sales.Entry(sale).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return sale;
        }
    }
}
