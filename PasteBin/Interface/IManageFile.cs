namespace PasteBinApi.Interface
{
    public interface IManageFile
    {
        Task<string> UploadFileAsync(IFormFile formFile);
        Task<(byte[], string, string)> DownloadFileAsync(string Filename);
    }
}
