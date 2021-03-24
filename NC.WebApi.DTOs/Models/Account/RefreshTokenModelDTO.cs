using System.ComponentModel.DataAnnotations;

namespace NC.WebApi.DTOs.Models.Account
{
    public class RefreshTokenModelDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
