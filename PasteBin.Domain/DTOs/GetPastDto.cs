using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.Domain.DTOs
{
    public class GetPastDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDelete { get; set; }
        public int Views { get; set; }
        public string HashUrl { get; set; }
        public string Text { get;set; } 
    }
}
