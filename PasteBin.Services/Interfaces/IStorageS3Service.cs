namespace PasteBin.Services.Interfaces
{
    public interface IStorageS3Service
    {
        Task<bool> UploadTextToStorageAsync(string textPasteBin, string key);
        Task<string> GetTextPasteToS3Async(string key);
        Task<bool> DeleteTextPasteToS3Async(string key);
    }
    
}