using Microsoft.AspNetCore.Identity;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Model;
using PasteBin.Services.Builder;
using PasteBin.Services.Interfaces;

namespace PasteBin.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenCreateService _tokenCreateService;

        public LoginService(UserManager<User> userManager,
         ITokenCreateService tokenCreateService
            )
        {
            _userManager = userManager;
            _tokenCreateService = tokenCreateService;
        }

        public async Task<IBaseResponse<string>> Login(LoginDto loginDto)
        {
            var response = BaseResponseBuilder<string>.GetBaseResponse();

            try
            {
                var user = await _userManager.FindByNameAsync(loginDto.Email);

                if (user == null)
                {
                    response.StatusCode = 400;
                    response.Description = "An error occurred during authorization, please check your details and try again later";
                    return response;
                }

                var passwordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!passwordCorrect)
                {
                    response.StatusCode = 400;
                    response.Description = "An error occurred during authorization, please check your details and try again later";
                    return response;
                }
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole == null)
                {
                    response.StatusCode = 404;
                    response.Description = "Error Login";
                    return response;
                }
                var token = _tokenCreateService.TokenCreate(user, userRole);

                response.StatusCode = 200;
                response.Data = token;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                return response;
            }
        }

    }
}
