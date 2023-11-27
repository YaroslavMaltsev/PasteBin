namespace PasteBinApi.ResourceModel
{
    public class CreatePastModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DtateCreate { get; set; } = DateTime.Now;
        public DateTime DateDeiete { get; set; }
        public string URL { get; set; }
        public string HashUrl { get; set; }
    }
}
