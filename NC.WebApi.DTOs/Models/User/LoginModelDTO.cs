using System.ComponentModel.DataAnnotations;

namespace NC.WebApi.DTOs.Models.User
{
    public class LoginModelDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
