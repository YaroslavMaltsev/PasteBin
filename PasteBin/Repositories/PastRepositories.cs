using Microsoft.EntityFrameworkCore;
using PasteBin.Data;
using PasteBin.Model;
using PasteBinApi.Interface;

namespace PasteBinApi.Repositories
{
    public class PastRepositories : IPastRepositiries
    {
        private readonly ApplicationDbContext _context;

        public PastRepositories(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePost(Past past)
        {
            await _context.AddAsync(past);

            return Save().Result;
        }

        public bool Delete(Past past)
        {
            var del = _context.Remove(past);

            return Save().Result;
        }

        public async Task<Past> GetPastById(int Id)
        {
            return await _context.Pasts.Where(i => i.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Past> GetPostByHash(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefaultAsync();
        }

        public bool HashExists(string hash)
        {
            var exists = _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefault();

            return exists != null ? true : false;
        }

        public bool PastExists(int Id)
        {
            var exists = _context.Pasts.Where(i => i.Id == Id).FirstOrDefault();

            return exists != null ? true : false;

        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();

            return saved != 0 ? true : false;

        }

        public bool UpdatePast(Past past)
        {
            var update =  _context.Update(past);

            return Save().Result;
        }
    }
}
