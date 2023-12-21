using PasteBin.Model;

namespace PasteBinApi.Interface
{
    public interface IPastRepositiries
    {
        public Task<Past> GetPostByHash(string hash);
        public Task<bool> CreatePost(Past past);
        Task<bool> Save();
        bool Delete(Past past);
        bool PastExists(int Id);
        public Task<Past> GetPastById(int Id);
        bool HashExists(string hash);
        public bool UpdatePast(Past past);
    }
}
