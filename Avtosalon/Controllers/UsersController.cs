using Avtosalon.Models.DTOs.Users;
using Avtosalon.Models.Users;
using Avtosalon.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avtosalon.Controllers
{
    [Authorize(Roles = "Director")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async ValueTask<ActionResult<GetUserDTO>> PostUserAsync(CreateUserDTO createUserDTO)
        {
            GetUserDTO postedUser = await userService.AddUserAsync(createUserDTO);

            return StatusCode(201, postedUser);
        }
                
        [HttpGet]
        public ActionResult<IQueryable<GetUserDTO>> GetAllUsers()
        {
            IQueryable<GetUserDTO> users = userService.RetrieveAllUsers();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async ValueTask<ActionResult<GetUserDTO>> GetUserById(Guid userId)
        {
            GetUserDTO getUser = await userService.RetrieveUserByIdAsync(userId);

            return Ok(getUser);
        }

        [HttpPut]
        public async ValueTask<ActionResult<GetUserDTO>> UpdateUserAsync(User user)
        {
            GetUserDTO getUserDTO = await userService.ModifyUserAsync(user);

            return Ok(getUserDTO);
        }

        [HttpDelete("{userId}")]
        public async ValueTask<ActionResult<GetUserDTO>> DeleteUserAsync(Guid userId)
        {
            GetUserDTO deletedUser = await userService.RemoveUserAsync(userId);

            return Ok(deletedUser);
        }
    }
}
