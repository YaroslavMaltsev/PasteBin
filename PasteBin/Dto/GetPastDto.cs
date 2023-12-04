using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PasteBinApi.Dto
{
    public class GetPastDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime DtateCreate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDelete { get; set; }
        public string HashUrl { get; set; }
    }
}
