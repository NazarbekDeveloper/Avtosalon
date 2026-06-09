using Avtosalon.Models.Sales;
using Avtosalon.Services.Sales;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Sale>> PostSaleAsync(Sale sale)
        {
            Sale addedSale = await saleService.AddSaleAsync(sale);

            return StatusCode(201, addedSale);
        }

        [HttpGet]
        public ActionResult<IQueryable<Sale>> GetAllSales()
        {
            IQueryable<Sale> sales = saleService.RetrieveAllSales();

            return Ok(sales);
        }

        [HttpGet("{saleId}")]
        public async ValueTask<ActionResult<Sale>> GetSaleByIdAsync(Guid saleId)
        {
            Sale maybeSale = await saleService.RetrieveSaleByIdAsync(saleId);

            return Ok(maybeSale);
        }

        [HttpPut]
        public async ValueTask<ActionResult<Sale>> PutSaleAsync(Sale sale)
        {
            Sale updatedSale = await saleService.ModifySaleAsync(sale);

            return Ok(updatedSale);
        }

        [HttpDelete("{saleId}")]
        public async ValueTask<ActionResult<Sale>> DeleteSaleAsync(Guid saleId)
        {
            Sale deletedSale = await saleService.RemoveSaleAsync(saleId);

            return Ok(deletedSale);
        }
    }
}
