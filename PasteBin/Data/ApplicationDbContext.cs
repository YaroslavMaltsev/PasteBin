using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PasteBin.Model;
using PasteBinApi.Model;

namespace PasteBin.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Past> Pasts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
