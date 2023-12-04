using Microsoft.AspNetCore.Identity;
using PasteBin.Model;

namespace PasteBinApi.Model
{
    public class User : IdentityUser
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public ICollection<Past> Pasts { get; set; }
    }
}
