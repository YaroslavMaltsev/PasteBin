using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.Dto;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastController(IPastRepositiries pastRepositories,
        ILogger<PastController> logger, IMapper mapper
        ) : ControllerBase
    {
        private readonly ILogger<PastController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        private readonly IPastRepositiries _pastRepositories = pastRepositories;

        [HttpGet]
        [Route("{id:int}", Name = "GetPastById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPastById(int id)
        {
            _logger.LogInformation("GetPastById method stared");// Сообщение что метод запущен
            if (!_pastRepositories.PastExists(id))
            {
                _logger.LogWarning("NotFound"); // Логирование ошибок

                return NotFound("Пост не найден");
            }
            var pastDto =_mapper.Map<GetPastDto>(await _pastRepositories.GetPastById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pastDto);
        }

        [HttpGet]
        [Route("{hash}", Name = "GetPastByHash")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePastDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetPostByHash(string hash) // добавить подключение к api
        {
            if (string.IsNullOrEmpty(hash))
                return BadRequest();

            if (!_pastRepositories.HashExists(hash))
                return NotFound($"Пост не найден");

            var post = _mapper.Map<GetPastDto>(await _pastRepositories.GetPostByHash(hash));

            if (!ModelState.IsValid)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//Ok
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromForm] CreatePastDto createPastModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var createPaste = _mapper.Map<Past>(createPastModel);

                if (createPaste == null)
                    return BadRequest(ModelState);

                await _pastRepositories.CreatePost(createPaste);
            }
            catch (Exception ex)
            {
                return BadRequest("При создание что-то полшло не так");
            }

            return NoContent();
            
        }
        [HttpDelete]
        [Route("{id:int}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BadRequest - документация ошибок
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NoContent
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NotFound
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost(int Id)
        {
            if (!_pastRepositories.PastExists(Id))
                return NotFound();

            var pastDelete = await _pastRepositories.GetPastById(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! _pastRepositories.Delete(pastDelete))
                ModelState.AddModelError("", "Что-то пошло не так при удалении поста");

            return NoContent();
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdatePaste")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PasteUpdate(int id, [FromBody] UpdatePastDto update)
        {
            if (id == null || id <= 0)
                return BadRequest();

            if (!_pastRepositories.PastExists(id))
                return NotFound();

            var paste = _pastRepositories.GetPastById(id);
            //try
            //{
               var updatePaste = _mapper.Map<Past>(update);

               if (_pastRepositories.UpdatePast(updatePaste))
                    ModelState.AddModelError("", "Что-то пошло не так при обновлении поста");
           // }
            //catch
            //{
             //   return BadRequest("Что-то пошло не так при обновление поста ");
            //}
            return NoContent();
        }
    }
}
