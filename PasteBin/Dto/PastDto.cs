using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.ResourceModel
{
    public class PastDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название поста не должно быть пустым")]
        [StringLength(30,ErrorMessage ="Пожалуста сократите название поста")]
        public string Title { get; set; }
        [Required]
        public DateTime DtateCreate { get; set; } 
        public DateTime DateDelete { get; set; }
        [Required]
        public string URL { get; set; }
        public string HashUrl { get; set; }
    }
}
