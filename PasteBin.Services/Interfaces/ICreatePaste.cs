using PasteBin.Domain.DTOs;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface ICreatePaste
    {
        Task<ResponseCreateDto> CreatePosteServiceAsync(CreatePasteDto pastCreate, string userId);
    }
}