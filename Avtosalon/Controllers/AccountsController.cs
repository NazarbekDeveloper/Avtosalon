using Avtosalon.Models.Exceptions;
using Avtosalon.Models.Users;
using Avtosalon.Services.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async ValueTask<ActionResult<UserToken>> LoginAsync([FromBody] UserCredential userCredential)

        {
            try
            {
                UserToken token = await this.accountService.LoginAsync(userCredential);

                return Ok(token);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}