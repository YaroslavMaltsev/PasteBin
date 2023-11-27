using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;

namespace PasteBinApi.Interface
{
    public interface IPastRepositiries
    {
        public Task<Past> GetPostHash(string hash);
        public bool CreatePost(Past past);
        public Task<IActionResult> DelitePost();
        bool Save();
        Task<bool> Delete();
    }
}
