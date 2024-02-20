namespace PasteBin.Services.CustomExptions
{
    public class StorageServiceException : Exception
    {
        public StorageServiceException(string? message) : base(message)
        {
        }
    }
}
