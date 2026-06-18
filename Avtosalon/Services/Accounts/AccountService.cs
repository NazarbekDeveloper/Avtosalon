using Avtosalon.Helpers;
using Avtosalon.Models.Exceptions;
using Avtosalon.Models.Users;
using Avtosalon.Repositories.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Avtosalon.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AccountService(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public async ValueTask<UserToken> LoginAsync(UserCredential userCredential)
        {
            User user =  userRepository.SelectAllUsers().FirstOrDefault(u => u.Username == userCredential.Username);

            if (user is null)
                throw new ValidationException("Username yoki Password xato kiritildi.");

            bool isPasswordEqual = HashingHelper.IsHashValid(userCredential.Password, user.PasswordHash);

            if (isPasswordEqual is false)
                throw new NotFoundException("Username yoki Password xato kiritildi.");

            return GenerateToken(user);
        }

        private UserToken GenerateToken(User user)
        {
            string issuer = configuration["JWTToken:Issuer"];
            string audience = configuration["JWTToken:Audience"];
            string key = configuration["JWTToken:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            DateTime expirationDate = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expirationDate,
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserToken
            {
                Token = tokenString,
                ExpirationDate = expirationDate
            };
        }
    }
}
