using PasteBin.Domain.Model;

namespace PasteBinApi.DAL.Interface
{
    public interface IPastRepositories
    {
        public Task<Past> GetPostByHash(string hash);
        public Task<bool> CreatePost(Past past);
        Task<bool> Save();
        bool Delete(Past past);
        public Task<Past> GetPastById(int Id);
        public bool UpdatePast(Past past);
        public Task<IEnumerable<Past>> GetPastAll();
    }
}
