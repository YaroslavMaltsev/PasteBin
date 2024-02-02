﻿namespace PasteBin.Services.Interfaces
{
    public interface IStorageS3Service
    {
        Task<bool> UploadTextToStorage(string textPasteBin, string key);
        Task<string> GetTextPasteToS3(string key);
    }
    
}