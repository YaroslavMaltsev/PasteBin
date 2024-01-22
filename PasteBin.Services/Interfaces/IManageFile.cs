using Microsoft.AspNetCore.Http;

namespace PasteBinApi.Services.Interface
{
    public interface IManageFile
    {
        Task<int> UpdateFileAsync(IFormFile formFile, string fileName);
        Task<string> UploadFileAsync(IFormFile formFile);
        Task<(byte[], string, string)> DownloadFileAsync(string fileName);
    }
}
