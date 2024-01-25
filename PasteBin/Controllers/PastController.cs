using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Domain.Model.RoleUsers;
using PasteBin.Services.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("PosteBin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PastController : ControllerBase
    {
        private readonly IPasteService _pasteService;

        public PastController(IPasteService pasteService)
        {
            _pasteService = pasteService;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetPastById")]
        [Authorize(Roles = StaticRoleUsers.USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPastById(int id)
        {
            string userid = HttpContext.User.FindFirst("UserId").ToString();

            var responsePaste = await _pasteService.GetPostByIdService(id,userid);

            if (responsePaste.StatusCode == 404)
                return BadRequest(responsePaste.Description);

            if (responsePaste.StatusCode == 400)
                return NotFound(responsePaste.Description);

            if (responsePaste.StatusCode == 500)
                return Problem(responsePaste.Description);

            return Ok(responsePaste);
        }

        [HttpGet]
        [Route("{hash}", Name = "GetPastByHash")]
        [Authorize(Roles = StaticRoleUsers.USER)]
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

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = StaticRoleUsers.USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreatePasteDto createPastDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string userid = HttpContext.User.FindFirst("UserId").ToString();

            var responseCreatePaste = await _pasteService.CreatePosteService(createPastDto,userid);

            if (responseCreatePaste.StatusCode == 400)
                return BadRequest(responseCreatePaste.Description);

            if (responseCreatePaste.StatusCode == 500)
                return Problem(responseCreatePaste.Description);

            return Ok(responseCreatePaste);

        }
        [HttpDelete]
        [Route("{id:int}", Name = "Delete")]
        [Authorize(Roles = StaticRoleUsers.USER)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePost(int id)
        {
            string userid = HttpContext.User.FindFirst("UserId").ToString();

            var responsePosteDelete = await _pasteService.DeletePostService(id,userid);

            if (responsePosteDelete.StatusCode == 400)
                return BadRequest(responsePosteDelete.Description);

            if (responsePosteDelete.StatusCode == 404)
                return NotFound(responsePosteDelete.Description);

            if (responsePosteDelete.StatusCode == 500)
                return Problem(responsePosteDelete.Description);

            return Ok(responsePosteDelete);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdatePaste")]
        [Authorize(Roles = StaticRoleUsers.USER)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PasteUpdate(int id,
          [FromBody] UpdatePasteDto update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string userid = HttpContext.User.FindFirst("UserId").ToString();

            var responseUpdatePaste = await _pasteService.UpdatePostService(update, id,userid);

            if (responseUpdatePaste.StatusCode == 400)
                return BadRequest(responseUpdatePaste.Description);

            if (responseUpdatePaste.StatusCode == 404)
                return NotFound(responseUpdatePaste.Description);

            if (responseUpdatePaste.StatusCode == 500)
                return Problem(responseUpdatePaste.Description);

            return Ok(responseUpdatePaste);

        }
    }
}
