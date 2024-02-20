using PasteBin.Domain.DTOs;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IPasteService
    {
        public Task<ResponseCreateDto> CreatePosteServiceAsync(CreatePasteDto past, string userId);
        public Task<GetPastDto> GetPostByIdServiceAsync(int id, string userId );
        public Task<GetPastDto> GetPostByHashServiceAsync(string hash);
        public Task UpdatePostServiceAsync(UpdatePasteDto past, int id, string userId);
        public Task DeletePostServiceAsync(int id, string userId);
        public Task<IEnumerable<GetPastDto>> GetPostAllServiceAsync(string userId);
    }
}
