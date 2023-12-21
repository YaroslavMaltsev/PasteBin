using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.DTOs
{
    public class CreatePasteDto
    {
        [Required(ErrorMessage = "Название поста не должно быть пустым")]
        [StringLength(30, ErrorMessage = "Пожалуйста сократите название поста")]
        public string Title { get; set; }
        [Required]
        public double DateSave { get; set; }
        public IFormFile formFile { get; set; }
    }
}
