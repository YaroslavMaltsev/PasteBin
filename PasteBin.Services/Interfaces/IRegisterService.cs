using PasteBin.Domain.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<IBaseResponse<bool>> RegisterUserAsync(RegisterDto registerDto);
    }
}