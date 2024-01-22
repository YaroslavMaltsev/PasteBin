using PasteBin.Domain.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IPasteService
    {
        public Task<IBaseResponse<bool>> CreatePosteService(CreatePasteDto past);
        public Task<IBaseResponse<GetPastDto>> GetPostByIdService(int id);
        public Task<IBaseResponse<GetPastDto>> GetPostByHashService(string hash);
        public Task<IBaseResponse<bool>> UpdatePostService(UpdatePasteDto past, int id);
        public Task<IBaseResponse<bool>> DeletePostService(int id);
        public Task<IBaseResponse<IEnumerable<GetPastDto>>> GetPostAllService();
    }
}
