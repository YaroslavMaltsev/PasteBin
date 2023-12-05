using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.Dto;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;
using PasteBinApi.Service;
using PasteBinApi.Services;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastController(IPastRepositiries pastRepositiries, ILogger<PastController> logger) : ControllerBase
    {
        private readonly ILogger<PastController> _logger = logger;
        private readonly IPastRepositiries _pastRepositiries = pastRepositiries;

        [HttpGet]
        [Route("{id:int}", Name = "GetPastById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPastDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPastById(int id)
        {
            _logger.LogInformation("GetPastById method stared");// Сообщение что метод запущен
            if (!_pastRepositiries.PastExists(id))
            {
                _logger.LogWarning("NotFound"); // Логирование ошибок

                return NotFound("Пост не найден");
            }
            var past = _pastRepositiries.GetPastById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pastDto = new GetPastDto
            {
                Id = past.Id,
                DateDelete = past.DateDelete.Date,
                Title = past.Title,
                DtateCreate = past.DtateCreate.Date,
                HashUrl =  past.HashUrl
            };
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

            if (!_pastRepositiries.HasрExists(hash))
                return NotFound($"Пост не найден");

            var post = await _pastRepositiries.GetPostByHash(hash);

            if (!ModelState.IsValid)
                return NotFound();

            return Ok(post);
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
            try
            {
                var newPast = new Past
                {
                    Title = createPastModel.Title,
                    DtateCreate = DateTime.UtcNow,
                    DateDelete = TimeCalculationService.GetTimeToDelete(createPastModel.DateSave, DateTime.Now),
                    URL = "string",// createPastModel.URL,// Создать сервис для загрузки с яндекс Cloud 
                    HashUrl = HashService.ToHash()
                };
                if (newPast == null)
                   return BadRequest(ModelState);
                
                _pastRepositiries.CreatePost(newPast);
            
            }
            catch
            {
                return BadRequest($"При создание поста что-то пошло не так");
            }

            return NoContent();
          //  return CreatedAtRoute("GetPastById", new { id = newPast.Id }, newPast);// Возврат созданного поста
        }
        [HttpDelete]
        [Route("{id:int}", Name = "Delete")]
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
                ModelState.AddModelError("", "Что-то пошло не так при удалении поста");
           
            return NoContent();
        }
        [HttpPatch]
        [Route("{id:int}",Name="Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PastUpdate (int id,[FromBody] JsonPatchDocument<UpdatePastDto> model)
        {
            if (model == null || id <= 0)
                return BadRequest();

            if(!_pastRepositiries.PastExists(id))
                return NotFound();

            var past = _pastRepositiries.GetPastById(id);

            var pastDto = new UpdatePastDto
            {
                Title = past.Title,
                DateDelete = past.DateDelete,
            };
            model.ApplyTo(pastDto, ModelState);
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            past.Title = pastDto.Title;
            past.DateDelete = pastDto.DateDelete;
            past.HashUrl = HashService.ToHash();
            
            if(_pastRepositiries.UpdatePast(past))
                ModelState.AddModelError("", "Что-то пошло не так при обновлении поста");
          
            return NoContent();
        }
    }
}
