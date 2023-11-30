using PasteBinApi.Validators;

namespace PasteBinApi.Dto
{
    public class GetPastDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DtateCreate { get; set; }
        [DateOfStorage]
        public DateTime DateDelete { get; set; }
        public string URL { get; set; }
        public string HashUrl { get; set; }
    }
}
