using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Services.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("pastebin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PastController : ControllerBase
    {


        public PastController()
        {
           
        }

        [HttpGet]
        [Route("get-paste-by-id/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPastById(int id)
        {
            if (id == 0)
            {
                var responsePaste = await _pasteService.GetPostAllServiceAsync(GetUserId());
               
                return Ok(responsePaste);
            }
            else 
            {
                var responsePaste = await _pasteService.GetPostByIdServiceAsync(id, GetUserId());
                
                return Ok(responsePaste);
            } 
        }

        [HttpPost]
        [Route("create-paste")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreatePasteDto createPastDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _pasteService.CreatePosteServiceAsync(createPastDto,GetUserId());
           
            return Ok(response);


        }
        [HttpDelete]
        [Route("delete-paste/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePost(int id)
        {

             await _pasteService.DeletePostServiceAsync(id, GetUserId());

            return NoContent();
        }
        [HttpPut]
        [Route("update-paste/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PasteUpdate(int id,
          [FromBody] UpdatePasteDto update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           await _pasteService.UpdatePostServiceAsync(update, id, GetUserId());

            return NoContent();

        }
        protected string GetUserId()
        {
            string user = this.User.Claims.First(i => i.Type == "UserId").Value;
            return user;
        }
    }
}
