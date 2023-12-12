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

        public Task<(byte[], string, string)> DownloadFileAsync(string Filename)
        {
            throw new NotImplementedException();
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

