using Avtosalon.Models.Sales;

namespace Avtosalon.Services.Sales
{
    public interface ISaleService
    {
        ValueTask<Sale> AddSaleAsync(Sale sale);
        IQueryable<Sale> RetrieveAllSales();
        ValueTask<Sale> RetrieveSaleByIdAsync(Guid saleId);
        ValueTask<Sale> ModifySaleAsync(Sale sale);
        ValueTask<Sale> RemoveSaleAsync(Guid saleId);
    }
}
