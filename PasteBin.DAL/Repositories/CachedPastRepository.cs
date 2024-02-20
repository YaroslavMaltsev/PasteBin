using PasteBin.Domain.Model;
using PasteBinApi.DAL.Interface;
using PasteBinApi.DAL.Repositories;

namespace PasteBin.DAL.Repositories
{
    public class CachedPastRepository : IPastRepositories
    {
        private readonly PastRepositories _pastRepositories;

        public CachedPastRepository(PastRepositories pastRepositories)
        {
            _pastRepositories = pastRepositories;
        }
        public async Task CreatePostAsync(Past past)
        {
            await _pastRepositories.CreatePostAsync(past);
        }

        public void Delete(Past past)
        {
            _pastRepositories.Delete(past);
        }

        public async Task<IEnumerable<Past>> GetPastAllAsync(string userId)
        {
            return await _pastRepositories.GetPastAllAsync(userId);
        }

        public async Task<Past> GetPastByIdAsync(int Id, string userId)
        {
          return await _pastRepositories.GetPastByIdAsync(Id, userId);
        }

        public async Task<Past> GetPostByHashAsync(string hash)
        {
            return await _pastRepositories.GetPostByHashAsync(hash);
        }

        public void UpdatePast(Past past)
        {
            _pastRepositories.UpdatePast(past);
        }
    }
}
