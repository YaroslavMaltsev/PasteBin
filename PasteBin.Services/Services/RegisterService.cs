using Microsoft.AspNetCore.Identity;
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Model.RoleUsers;
using PasteBin.Services.Builder;
using PasteBin.Services.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IBaseResponse<bool>> RegisterUser(RegisterDto registerDto)
        {
            var response = BaseResponseBuilder<bool>.GetBaseResponse();
            try
            {
                var ExistUser = await _userManager.FindByEmailAsync(registerDto.Email);

                if (ExistUser != null)
                {
                    response.StatusCode = 404;
                    response.Description = "A user with the same Email already exists";
                    response.Data = false;
                    return response;
                }

                var newUser = new IdentityUser()
                {
                    Email = registerDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

                if (createUserResult.Succeeded == false)
                {
                    response.StatusCode = 404;
                    response.Description = "There was an error creating the user";
                    response.Data = false;
                    return response;
                }

                await _userManager.AddToRoleAsync(newUser, StaticRoleUsers.USER);

                response.StatusCode = 200;
                response.Description = "User creation was successful";
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
