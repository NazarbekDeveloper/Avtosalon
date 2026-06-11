using System.ComponentModel.DataAnnotations;

namespace Avtosalon.Models.Users
{
    public class UserCredential
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
