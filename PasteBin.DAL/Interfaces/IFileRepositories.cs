namespace PasteBin.DAL.Interfaces
{
    public interface IFileRepositories
    {
        bool Delete(string fileName);
        Task<bool> UpdateAsync(string fileName);
        Task<(byte[], string, string)> DownloadAsync(string fileName);
    }
}