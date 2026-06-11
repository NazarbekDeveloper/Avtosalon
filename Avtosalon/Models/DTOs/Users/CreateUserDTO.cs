using Avtosalon.Models.Users;

namespace Avtosalon.Models.DTOs.Users
{
    public class CreateUserDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  
        public string Phone { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
