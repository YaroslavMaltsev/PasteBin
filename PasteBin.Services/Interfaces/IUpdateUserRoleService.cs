using PasteBin.Domain.DTOs;
using PasteBin.Domain.Interfaces;

namespace PasteBin.Services.Services
{
    public interface IUpdateUserRoleService
    {
        Task<IBaseResponse<bool>> UpdateUserRoleAsync(UpdateRoleDto updateRoleDto);
    }
}