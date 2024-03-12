using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IUpdatePaste
    {
        Task UpdatePostServiceAsync(UpdatePasteDto updatePast, int id, string userId);
    }
}