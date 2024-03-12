using PasteBin.Domain.Model;

namespace PasteBinApi.DAL.Interface
{
    public interface IPastRepositories
    {
        public Task<Past> GetPostByHashAsync(string hash);
        public Task CreatePostAsync(Past past);
        public void Delete(Past past);
        public Task<Past> GetPastByIdAsync(int Id, string userId);
        public void UpdatePast(Past past);
        public Task<IEnumerable<Past>> GetPastAllAsync(string userId);
    }
}
