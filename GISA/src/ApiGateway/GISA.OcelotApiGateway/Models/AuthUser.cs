using System.ComponentModel.DataAnnotations;

namespace GISA.OcelotApiGateway.Models
{
    public class AuthUser
    {
        [Required]
        public string Username { get; set; }
       
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
