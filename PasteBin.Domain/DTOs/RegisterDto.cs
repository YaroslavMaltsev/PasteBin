using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasteBinApi.Domain.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]   
        public string PasswordConfirm { get; set; }

    }
}
