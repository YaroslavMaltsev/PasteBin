using Microsoft.AspNetCore.Mvc;
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

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DelitePost()
        {
            throw new NotImplementedException();
        }

        public async Task<Past> GetPostHash(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash && i.DateDeiete > DateTime.Now).FirstOrDefaultAsync();

        }

        public bool Save()
        {
            var saved =  _context.SaveChanges();
            return saved != 0 ? true : false;
        }
    }
}
