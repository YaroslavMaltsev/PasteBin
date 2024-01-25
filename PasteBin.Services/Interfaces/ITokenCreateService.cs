using Microsoft.AspNetCore.Identity;

namespace PasteBin.Services.Interfaces
{
    public interface ITokenCreateService
    {
      public string TokenCreate(IdentityUser user, IList<string> identityRoles);
    }
}