using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IGetPasteByHash
    {
        Task<GetPastDto> GetPostByHashServiceAsync(string hash);
    }
}