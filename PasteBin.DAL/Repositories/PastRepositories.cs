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
        public async Task<bool> CreatePost(Past past)
        {
            await _context.AddAsync(past);

            return await Save();
        }

        public bool Delete(Past past)
        {
            var del = _context.Remove(past);

            return Save().Result;
        }

        public async Task<Past> GetPastById(int Id,string userId)
        {
            return await _context.Pasts.Where(i => i.Id == Id ).
                Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Past>> GetPastAll(string userId)
        {
            return await _context.Pasts.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Past> GetPostByHash(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();

            return saved != 0 ? true : false;

        }

        public bool UpdatePast(Past past)
        {
            var update = _context.Update(past);

            return Save().Result;
        }
    }
}
