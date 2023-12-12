using PasteBinApi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasteBin.Model
{
    public class Past
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DtateCreate { get; set; }
        public DateTime DateDelete { get; set; }
        public string URL { get; set; }
        public string HashUrl { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
