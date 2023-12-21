using Microsoft.AspNetCore.StaticFiles;
using PasteBinApi.Interface;


namespace PasteBinApi.Services
{
    public class ManageFile : IManageFile
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManageFile(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<(byte[],string,string)> DownloadFileAsync(string fileName)
        {

            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads",fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return (bytes, contenttype, Path.GetFileName(path));

        }

        public async Task<int> UpdateFileAsync(IFormFile formFile, string fileName)
        {
            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            var file = Directory.GetFiles(path, fileName).FirstOrDefault();
            if (file == null)
                return 0;

            var ext = Path.GetExtension(formFile.FileName);
            var allowedExtensions = new string[] { ".json", ".doc", ".txt", ".JPG" };

            if (!allowedExtensions.Contains(ext))
                return 0;

            var fileWithPath = Path.Combine(path, fileName);
            using (var fs = new FileStream(fileWithPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fs);
            }
            return 1;
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var ext = Path.GetExtension(formFile.FileName);
            var allowedExtensions = new string[] { ".json", ".doc", ".txt", ".JPG" };

            if (!allowedExtensions.Contains(ext))
                throw new Exception("Не правильный формат файла");

            string uniqueString = Guid.NewGuid().ToString();

            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            using (var fs = new FileStream(fileWithPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fs);
            }
            return newFileName;

        }
    }
}

