using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastController(IPastRepositiries pastRepositories,
        ILogger<PastController> logger, IMapper mapper, ITimeCalculationService timeCalculation,
        IHashService hashService
        ) : ControllerBase
    {
        private readonly ILogger<PastController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly ITimeCalculationService _timeCalculation = timeCalculation;
        private readonly IHashService _hashService = hashService;
        private readonly IPastRepositiries _pastRepositories = pastRepositories;

        [HttpGet]
        [Route("{id:int}", Name = "GetPastById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPastById(int id)
        {
            _logger.LogInformation("GetPastById method stared");// Сообщение что метод запущен
            if (!_pastRepositories.PastExists(id))
            {
                _logger.LogWarning("NotFound"); // Логирование ошибок

                return NotFound("Пост не найден");
            }
            var pastDto = _mapper.Map<GetPastDto>(await _pastRepositories.GetPastById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pastDto);
        }

        [HttpGet]
        [Route("{hash}", Name = "GetPastByHash")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
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
        public async Task<ActionResult> Create([FromForm] CreatePasteDto createPastModel)
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
            catch
            {
                return BadRequest("При создание что-то пошло не так");
            }

            return NoContent();

        }
        [HttpDelete]
        [Route("{id:int}/Delete", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (!_pastRepositories.PastExists(id))
                return NotFound();

            var pastDelete = await _pastRepositories.GetPastById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pastRepositories.Delete(pastDelete))
                return BadRequest("Что-то пошло не так при удалении поста");

            return NoContent();
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdatePaste")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PasteUpdate(int id,
          [FromBody] UpdatePasteDto update)
        {
            if (id == null || id <= 0)
                return BadRequest();

            if (!_pastRepositories.PastExists(id))
                return NotFound();

            var paste = _pastRepositories.GetPastById(id).Result;

            paste.Title = update.Title;
            paste.HashUrl = _hashService.ToHash();
            paste.DateDelete = _timeCalculation.GetTimeToDelete(update.DateSave);

            if (_pastRepositories.UpdatePast(paste))
                return BadRequest("Что-то пошло не так при обновлении поста");

            return NoContent();
        }


    }
}
