using Microsoft.AspNetCore.Mvc;
using PasteBin.Model;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;
using PasteBinApi.Service;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PastController(IPastRepositiries pastRepositiries) : Controller
    {
        private readonly IPastRepositiries _pastRepositiries = pastRepositiries;

        [HttpGet("{hash}")]
        [ProducesResponseType(200, Type = typeof(Past))]
        public async Task<IActionResult> GetPostToHash(string hash) // добавить подключение к api
        {
            var post = await _pastRepositiries.GetPostHash(hash);
            if (!ModelState.IsValid)
                return NotFound("Пост не найден");
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create ( [FromBody] CreatePastModel createPastModel)
        {
            if(ModelState.IsValid)
            {
                var newPast = new Past
                {
                    Title = createPastModel.Title,
                    DateDeiete = createPastModel.DateDeiete,
                    URL = createPastModel.URL,// Создать сервис для загрузки с яндекс Cloud 
                    HashUrl = HashService.ToHash(createPastModel.DtateCreate.ToString())
                };
                if(newPast == null)
                    return BadRequest(ModelState);
                _pastRepositiries.CreatePost(newPast);
            }
          
            return Ok();
        }

    }
}
