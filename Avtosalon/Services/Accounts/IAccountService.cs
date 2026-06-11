using Avtosalon.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Avtosalon.Services.Accounts
{
    public interface IAccountService
    {
        ValueTask<UserToken> LoginAsync(UserCredential userCredential);
    }
}
