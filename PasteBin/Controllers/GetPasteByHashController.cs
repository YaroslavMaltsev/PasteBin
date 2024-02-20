using Microsoft.AspNetCore.Mvc;
using PasteBin.Services.Interfaces;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("pastebin/[controller]")]
    public class GetPasteByHashController : ControllerBase
    {
        private readonly IPasteService _pasteService;

        public GetPasteByHashController(IPasteService pasteService)
        {
            _pasteService = pasteService;
        }
        [HttpGet]
        [Route("get-past-by-hash/{hash}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPostByHash(string hash)
        {
            var responsePaste = await _pasteService.GetPostByHashServiceAsync(hash);
        
            return Ok(responsePaste);
        }
    }
}
