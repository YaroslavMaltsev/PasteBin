using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;

namespace PasteBinApi.Interface
{
    public interface IPastRepositiries
    {
        public Task<Past> GetPostHash(string hash);
        public Task<IActionResult> CreatePost();
        public Task<IActionResult> DelitePost();
    }
}
