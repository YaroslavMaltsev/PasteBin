namespace PasteBin.Services.Interfaces
{
    public interface IDeletePaste
    {
        Task DeletePostServiceAsync(int id, string userId);
    }
}