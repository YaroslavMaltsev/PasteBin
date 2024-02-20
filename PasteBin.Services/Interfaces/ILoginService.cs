using Microsoft.AspNetCore.Identity;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Interfaces;

namespace PasteBin.Services.Interfaces
{
    public interface ILoginService
    {
        Task<IBaseResponse<string>> LoginAsync(LoginDto loginDto);
    }
}