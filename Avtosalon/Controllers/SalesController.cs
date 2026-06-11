using Avtosalon.Models.Sales;
using Avtosalon.Services.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Director, Manager, Seller")]
        public async ValueTask<ActionResult<Sale>> PostSaleAsync(Sale sale)
        {
            Sale addedSale = await saleService.AddSaleAsync(sale);

            return StatusCode(201, addedSale);
        }

        [HttpGet]
        [Authorize(Roles = "Director, Manager, Seller")]
        public ActionResult<IQueryable<Sale>> GetAllSales()
        {
            IQueryable<Sale> sales = saleService.RetrieveAllSales();

            return Ok(sales);
        }

        [HttpGet("{saleId}")]
        [Authorize(Roles = "Director, Manager, Seller")]
        public async ValueTask<ActionResult<Sale>> GetSaleByIdAsync(Guid saleId)
        {
            Sale maybeSale = await saleService.RetrieveSaleByIdAsync(saleId);

            return Ok(maybeSale);
        }

        [HttpPut]
        [Authorize(Roles = "Director")]
        public async ValueTask<ActionResult<Sale>> PutSaleAsync(Sale sale)
        {
            Sale updatedSale = await saleService.ModifySaleAsync(sale);

            return Ok(updatedSale);
        }

        [HttpDelete("{saleId}")]
        [Authorize(Roles = "Director")]
        public async ValueTask<ActionResult<Sale>> DeleteSaleAsync(Guid saleId)
        {
            Sale deletedSale = await saleService.RemoveSaleAsync(saleId);

            return Ok(deletedSale);
        }
    }
}
