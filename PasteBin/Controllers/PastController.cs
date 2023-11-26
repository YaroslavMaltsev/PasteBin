using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.Interface;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastController(IPastRepositiries pastRepositiries) : Controller
    {
        private readonly IPastRepositiries _pastRepositiries = pastRepositiries;

        [HttpGet("{hash}")]
        [ProducesResponseType(200, Type = typeof(Past))]
        public async Task<IActionResult> GetPostToHash(string hash)
        {
            var post = await _pastRepositiries.GetPostHash(hash);
            if (!ModelState.IsValid)
                return NotFound();
            return Ok(post);
        }


    }
}
