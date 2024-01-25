using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasteBin.Domain.Model
{
    public class Past
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateDelete { get; set; }
        public string URL { get; set; }
        public string HashUrl { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
