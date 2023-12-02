using PasteBin.Model;

namespace PasteBinApi.Interface
{
    public interface IPastRepositiries
    {
        public Task<Past> GetPostByHash(string hash);
        public bool CreatePost(Past past);
        bool Save();
        bool Delete(Past past);
        bool PastExists(int Id);
        public Past GetPastById(int Id);
        bool HasрExists(string hash);
        public bool UpdatePast(Past past);
    }
}
