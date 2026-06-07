using Avtosalon.Models.Exceptions;
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
            try
            {
                Sale addedSale = await saleService.AddSaleAsync(sale);

                return StatusCode(201, addedSale);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi.");
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Sale>> GetAllSales()
        {
            try
            {
                IQueryable<Sale> sales = saleService.RetrieveAllSales();

                return Ok(sales);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi.");
            }
        }

        [HttpGet("{saleId}")]
        public async ValueTask<ActionResult<Sale>> GetSaleByIdAsync(Guid saleId)
        {
            try
            {
                Sale maybeSale = await saleService.RetrieveSaleByIdAsync(saleId);

                return Ok(maybeSale);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi.");
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Sale>> PutSaleAsync(Sale sale)
        {
            try
            {
                Sale updatedSale = await saleService.ModifySaleAsync(sale);

                return Ok(updatedSale);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi.");
            }
        }

        [HttpDelete("{saleId}")]
        public async ValueTask<ActionResult<Sale>> DeleteSaleAsync(Guid saleId)
        {
            try
            {
                Sale deletedSale = await saleService.RemoveSaleAsync(saleId);

                return Ok(deletedSale);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi.");
            }
        }
    }
}
