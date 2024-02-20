using Microsoft.EntityFrameworkCore;
using PasteBin.DAL.Data;
using PasteBin.Domain.Model;
using PasteBinApi.DAL.Interface;

namespace PasteBinApi.DAL.Repositories
{
    public class PastRepositories : IPastRepositories
    {
        private readonly ApplicationDbContext _context;

        public PastRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(Past past)
        {
            await _context.AddAsync(past);
        }

        public void Delete(Past past)
        {
             _context.Remove(past);

        }

        public async Task<Past> GetPastByIdAsync(int Id, string userId)
        {
            return await _context.Pasts.Where(i => i.Id == Id).
                Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Past>> GetPastAllAsync(string userId)
        {
            return await _context.Pasts.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Past> GetPostByHashAsync(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefaultAsync();
        }

        public void UpdatePast(Past past)
        {
            _context.Update(past);
        }

    }
}
