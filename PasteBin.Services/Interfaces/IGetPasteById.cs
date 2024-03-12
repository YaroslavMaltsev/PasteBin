using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IGetPasteById
    {
        Task<GetPastDto> GetPostByIdServiceAsync(int id, string userId);
    }
}