using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.Domain.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
