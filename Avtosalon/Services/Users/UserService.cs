using Avtosalon.Helpers;
using Avtosalon.Models.DTOs.Users;
using Avtosalon.Models.Exceptions;
using Avtosalon.Models.Users;
using Avtosalon.Repositories.Users;

namespace Avtosalon.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;   
        }

        public async ValueTask<GetUserDTO> AddUserAsync(CreateUserDTO createUerDTO)
        {
            if (createUerDTO is null)
                throw new ValidationException("CreateUser null bo'lishi mumkun emas.");

            if (string.IsNullOrWhiteSpace(createUerDTO.Username))
                throw new ValidationException("Username bo'sh bo'lishi mumkun emas.");

            User user = MapToUser(createUerDTO);
            await userRepository.InsertUserAsync(user);

            return MapToGetUserDTO(user);            
        }

        public IQueryable<GetUserDTO> RetrieveAllUsers() =>
           userRepository.SelectAllUsers().Select(user => MapToGetUserDTO(user));

        public async ValueTask<GetUserDTO> RetrieveUserByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ValidationException("UserId bo'sh bo'lishi mumkun emas.");

            User maybeUser = await userRepository.SelectUserByIdAsync(userId);

            if (maybeUser is null)
                throw new NotFoundException($"{userId} id bilan User topilmaid.");

            return MapToGetUserDTO(maybeUser);
        }

        public async ValueTask<GetUserDTO> ModifyUserAsync(User user)
        {
            if(user is null) 
                throw new ValidationException("User null bo'lishi mumkun emas.");

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ValidationException("Username bo'sh bo'lishi mumkin emas.");

            if (string.IsNullOrWhiteSpace(user.FullName))
                throw new ValidationException("FullName bo'sh bo'lishi mumkun emas.");

            User maybeUser = await userRepository.UpdateUserAsync(user);

            if (maybeUser is null)
                throw new NotFoundException($"{user.Id} id bilan User topilmadi.");

            return MapToGetUserDTO(maybeUser);
        }

        public async ValueTask<GetUserDTO> RemoveUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ValidationException("userId bo'sh bo'lishi mumkun emas.");

            User maybeUser = await userRepository.SelectUserByIdAsync(userId);

            if (maybeUser is null)
                throw new NotFoundException($"{userId} id bilan User topilmadi.");

            User deletedUser = await userRepository.DeleteUserAsync(maybeUser);

            return MapToGetUserDTO(deletedUser);
        }

        public static User MapToUser(CreateUserDTO createUserDTO)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Guid newId = Guid.NewGuid();

            return new User
            {
                Id = newId,
                FullName = createUserDTO.FullName,
                Username = createUserDTO.Username,
                PasswordHash = HashingHelper.GetHash(createUserDTO.Password),
                Phone = createUserDTO.Phone,
                Email = createUserDTO.Email,
                IsActive = true,
                Role = createUserDTO.Role,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }

        public static GetUserDTO MapToGetUserDTO(User user)
        {
            return new GetUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                Phone = user.Phone,
                Email = user.Email,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}
