using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasteBin.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PasteBin.Services.Services
{
    public class TokenCreateService : ITokenCreateService
    {
        private readonly IConfiguration _configuration;

        public TokenCreateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenCreate(IdentityUser user, IList<string> identityRoles)
        {
            string token = TokenCreateImplementation(user, identityRoles);

            return token;
        }

       private string TokenCreateImplementation(IdentityUser user, IList<string> identityRoles)
        {
            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id ),
               // new Claim("Email", user.Email),
                new Claim("JWTID", Guid.NewGuid().ToString())
            };

            foreach (var role in identityRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));

            var tokenObject = new JwtSecurityToken
            (
            issuer: _configuration["JwtConfig:ValidIssuer"],
            audience: _configuration["JwtConfig:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaim,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
