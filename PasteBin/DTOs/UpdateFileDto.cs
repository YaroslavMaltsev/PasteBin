using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.DTOs
{
    public class UpdateFileDto
    {
       
        [Required]
        public IFormFile formFile { get; set; }
    }
}
