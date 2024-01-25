
using Microsoft.AspNetCore.Identity;

namespace PasteBin.Domain.Model
{
    public class User : IdentityUser
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
