using Microsoft.AspNetCore.Mvc;
using PasteBin.Services.Interfaces;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("PasteBin/[controller]")]
    public class GetPasteByHashController : ControllerBase
    {
        private readonly IPasteService _pasteService;

        public GetPasteByHashController(IPasteService pasteService)
        {
            _pasteService = pasteService;
        }
        [HttpGet]
        [Route("{hash}", Name = "GetPastByHash")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPostByHash(string hash)
        {
            var responsePaste = await _pasteService.GetPostByHashService(hash);

            if (responsePaste.StatusCode == 400)
                return BadRequest(responsePaste.Description);

            if (responsePaste.StatusCode == 404)
                return NotFound(responsePaste.Description);

            if (responsePaste.StatusCode == 500)
                return Problem(responsePaste.Description);

            return Ok(responsePaste);
        }
    }
}
