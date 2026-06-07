using Avtosalon.Models.Persons;
using Avtosalon.Models.Sales;

namespace Avtosalon.Repositories.Sales
{
    public interface ISaleRepository
    {
        ValueTask<Sale> InsertSaleAsync(Sale sale);
        IQueryable<Sale> SelectAllSales();
        ValueTask<Sale> SelectSaleByIdAsync(Guid saleId);
        ValueTask<Sale> UpdateSaleAsync(Sale sale);
        ValueTask<Sale> DeleteSaleAsync(Sale sale);
    }
}
