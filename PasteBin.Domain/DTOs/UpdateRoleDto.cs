
using System.ComponentModel.DataAnnotations;

namespace PasteBin.Domain.DTOs
{
    public class UpdateRoleDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
