using Microsoft.AspNetCore.Mvc;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileRepositories _fileRepositories;

        public FileController(IFileRepositories fileRepositories)
        {
            _fileRepositories = fileRepositories;
        }

        [HttpGet]
        [Route("{fileName}/DownloadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            if (fileName == null)
                return BadRequest("Ведите название файла");
            try
            {
                var fileDownload = await _fileRepositories.DownloadAsync(fileName);

                if (fileName == null)
                    return NotFound();

                return File(fileDownload.Item1, fileDownload.Item2, fileDownload.Item3);
            }
            catch
            {
                return BadRequest("Что-то пошло не так при отправки файла. Проверьте имя файла");
            }
        }

        [HttpPut]
        [Route("{fileName}/Update", Name = "UpdateFile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateFile( string fileName,
            [FromQuery] UpdateFileDto update)
        {
            if (fileName == null)
                return BadRequest("Ведите имя файла");

            if (!ModelState.IsValid)
                return BadRequest("Произошла ошибка при получение файла");

            if (!await _fileRepositories.UpdateAsync(update.formFile, fileName))
                return BadRequest("Что-то пошло не так при обновление поста ");

            return NoContent();
        }
    }
}
