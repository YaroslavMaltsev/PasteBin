
using Microsoft.AspNetCore.Identity;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Model;
using PasteBin.Domain.Model.RoleUsers;
using PasteBin.Services.Builder;

namespace PasteBin.Services.Services
{
    public class UpdateUserRoleService : IUpdateUserRoleService
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserRoleService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IBaseResponse<bool>> UpdateUserRole(UpdateRoleDto updateRoleDto)
        {
            var response = BaseResponseBuilder<bool>.GetBaseResponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(updateRoleDto.Email);

                if (user == null)
                {
                    response.StatusCode = 400;
                    response.Description = "User NotFound";
                    response.Data = false;
                    return response;

                }
                await _userManager.AddToRoleAsync(user, StaticRoleUsers.ADMIN);

                response.StatusCode = 200;
                response.Description = "User role updated";
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                response.Data = false;
                return response;
            }

        }
    }
}
