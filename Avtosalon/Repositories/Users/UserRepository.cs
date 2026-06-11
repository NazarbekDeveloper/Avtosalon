using Avtosalon.Data;
using Avtosalon.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Avtosalon.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<User> InsertUserAsync(User user)
        {
            this.applicationDbContext.Users.AddAsync(user);
            await this.applicationDbContext.SaveChangesAsync();

            return user;
        }

        public IQueryable<User> SelectAllUsers() =>
            applicationDbContext.Users;

        public async ValueTask<User> SelectUserByIdAsync(Guid userId) =>
            await applicationDbContext.Users.FindAsync(userId);

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            applicationDbContext.Users.Entry(user).State = EntityState.Modified;
            await applicationDbContext.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            applicationDbContext.Users.Entry(user).State = EntityState.Deleted;
            await applicationDbContext.SaveChangesAsync();

            return user;
        }
    }
}
