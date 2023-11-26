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
        public Task<IActionResult> CreatePost()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DelitePost()
        {
            throw new NotImplementedException();
        }

        public async Task<Past> GetPostHash(string hash)
        {
            return await _context.Pasts.Where(i => i.HashUrl == hash).FirstOrDefaultAsync();
        }
    }
}
