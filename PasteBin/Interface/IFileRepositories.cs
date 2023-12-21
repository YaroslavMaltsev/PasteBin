namespace PasteBinApi.Interface
{
    public interface IFileRepositories
    {
        bool Delete(IFormFile formFile);
        Task<bool> UpdateAsync(IFormFile formFile, string fileName);
        Task<(byte[], string, string)> DownloadAsync(string fileName);
    }
}