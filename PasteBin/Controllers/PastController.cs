using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.Dto;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;
using PasteBinApi.Service;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastController(IPastRepositiries pastRepositiries) : Controller
    {
        private readonly IPastRepositiries _pastRepositiries = pastRepositiries;

        [HttpGet]
        [Route("{id:int}", Name = "GetPastById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Past))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPastById(int id)
        {
            if (!_pastRepositiries.PastExists(id))
                return NotFound("Пост не найден");

            var past = _pastRepositiries.GetPastById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(past);
        }

        [HttpGet]
        [Route("{hash}", Name = "GetPastByHash")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPostByHash(string hash) // добавить подключение к api
        {
            if (string.IsNullOrEmpty(hash))
                return BadRequest();
            if (!_pastRepositiries.HasрExists(hash))
                return NotFound($"Пост не найден");
            var post = await _pastRepositiries.GetPostByHash(hash);
            var getPast = new GetPastDto
            {
                Id = post.Id,
                Title = post.Title,
                DateDelete = post.DateDeiete,
                DtateCreate = post.DateDeiete,
                URL = post.URL,
                HashUrl = post.HashUrl
            };
            if (!ModelState.IsValid)
                return NotFound();
            return Ok(getPast);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]//Ok
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreatePastDto createPastModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newPast = new Past
            {
                Title = createPastModel.Title,
                DateDeiete = createPastModel.DateDelete,
                DtateCreate = DateTime.Now,
                URL = createPastModel.URL,// Создать сервис для загрузки с яндекс Cloud 
                HashUrl = HashService.ToHash(createPastModel.DtateCreate.ToString())
            };
            if (newPast == null)
                return BadRequest(ModelState);
            _pastRepositiries.CreatePost(newPast);


            return CreatedAtRoute("PastToId", new {id = newPast.Id }, newPast);// Возврат соданого поста
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BadRequest - документация ошибок
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NoContent
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NotFound
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePost(int Id)
        {
            if (!_pastRepositiries.PastExists(Id))
                return NotFound();

            var pastDelete = _pastRepositiries.GetPastById(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pastRepositiries.Delete(pastDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении поста");
            }
            return NoContent();
        }

    }
}
