using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Model;
using PasteBin.Domain.Model.RoleUsers;
using PasteBin.Services.Builder;
using PasteBin.Services.Interfaces;
namespace PasteBin.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
        }

        public async Task<IBaseResponse<bool>> CreateRole()
        {
            var response = BaseResponseBuilder<bool>.GetBaseResponse();
            try
            {
                bool isAdminRoleExist = await _roleManager.RoleExistsAsync(StaticRoleUsers.ADMIN);
                bool isUserRoleExist = await _roleManager.RoleExistsAsync(StaticRoleUsers.USER);

                if (isAdminRoleExist && isUserRoleExist)
                {
                    response.StatusCode = 200;
                    response.Description = "Roles have already been created";
                    response.Data = true;
                    return response;
                }

                await _roleManager.CreateAsync(new IdentityRole(StaticRoleUsers.USER));
                await _roleManager.CreateAsync(new IdentityRole(StaticRoleUsers.ADMIN));

                response.StatusCode = 200;
                response.Data = true;
                response.Description = "Role Create";
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
