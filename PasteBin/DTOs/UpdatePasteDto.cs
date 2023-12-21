using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.ResourceModel
{
    public class UpdatePasteDto
    {
        [Required(ErrorMessage = "Название поста не должно быть пустым")]
        [StringLength(30, ErrorMessage = "Пожалуйста сократите название поста")]
        public string Title { get; set; }
        [Required]
        public double DateSave { get; set; }
    }
}
