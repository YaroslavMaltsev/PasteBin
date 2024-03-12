using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IGetPasteAll
    {
        Task<IEnumerable<GetPastDto>> GetPostAllServiceAsync(string userId);
    }
}