using Avtosalon.Models.DTOs.Users;
using Avtosalon.Models.Users;

namespace Avtosalon.Services.Users
{
    public interface IUserService
    {
        ValueTask<GetUserDTO> AddUserAsync(CreateUserDTO createUerDTO);
        IQueryable<GetUserDTO> RetrieveAllUsers();
        ValueTask<GetUserDTO> RetrieveUserByIdAsync(Guid userId);
        ValueTask<GetUserDTO> ModifyUserAsync(User user);
        ValueTask<GetUserDTO> RemoveUserAsync(Guid userId);
    }
}
