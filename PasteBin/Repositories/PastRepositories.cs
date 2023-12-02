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
        public bool CreatePost(Past past)
        {
            _context.Add(past);

            return Save();
        }

        public bool Delete(Past past)
        {
            var del = _context.Remove(past);

            return Save();
        }

        public Past GetPastById(int Id)
        {
            return _context.Pasts.Where(i => i.Id == Id).FirstOrDefault();
        }

        public async Task<Past> GetPostByHash(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefaultAsync();
        }

        public bool HasрExists(string hash)
        {
            var exitsts = _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefault();

           return exitsts != null ? true : false;
        }

        public bool PastExists(int Id)
        {
            var exitsts = _context.Pasts.Where(i => i.Id == Id).FirstOrDefault();

            return exitsts != null ? true : false;

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved != 0 ? true : false;

        }

        public bool UpdatePast(Past past)
        {
            var update = _context.Update(past);

            return Save();
        }
    }
}
