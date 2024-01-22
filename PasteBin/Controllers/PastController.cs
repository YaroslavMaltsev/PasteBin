//using Microsoft.AspNetCore.Mvc;

//namespace PasteBinApi.Controllers
//{
//    [ApiController]
//    [Route("PosteBin/[controller]")]
//    public class PastController() : ControllerBase
//    {

//        [HttpGet]
//        [Route("{id:int}", Name = "GetPastById")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult> GetPastById(int id)
//        {
            
//        }

//        [HttpGet]
//        [Route("{hash}", Name = "GetPastByHash")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        public async Task<ActionResult> GetPostByHash(string hash)
//        {

//        }

//        [HttpPost]
//        [Route("Create")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<ActionResult> Create([FromForm] CreatePasteDto createPastModel)
//        {

//        }
//        [HttpDelete]
//        [Route("{id:int}/Delete", Name = "Delete")]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult> DeletePost(int id)
//        {

//        }
//        [HttpPut]
//        [Route("{id:int}", Name = "UpdatePaste")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public ActionResult PasteUpdate(int id,
//          [FromBody] UpdatePasteDto update)
//        {

//        }
//    }
//}
