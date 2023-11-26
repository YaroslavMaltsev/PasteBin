

namespace PasteBin.Model
{
    public class Past
    {

        public int Id { get; set; }
  
        public string Title {  get; set; }
        public DateTime DtateCreate { get; set; }
        public DateTime DateDeiete { get; set; }
        public string URL { get; set; }
        public string HashUrl { get; set; }
    }
}
