using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasteBin.Domain.Model
{
    public class Past
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public DateTime DateDelete { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public string HashUrl { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
