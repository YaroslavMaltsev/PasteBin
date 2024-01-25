using PasteBin.Domain.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IPasteService
    {
        public Task<IBaseResponse<bool>> CreatePosteService(CreatePasteDto past, string userId);
        public Task<IBaseResponse<GetPastDto>> GetPostByIdService(int id, string userId );
        public Task<IBaseResponse<GetPastDto>> GetPostByHashService(string hash);
        public Task<IBaseResponse<bool>> UpdatePostService(UpdatePasteDto past, int id, string userId);
        public Task<IBaseResponse<bool>> DeletePostService(int id, string userId);
        public Task<IBaseResponse<IEnumerable<GetPastDto>>> GetPostAllService(string userId);
    }
}
