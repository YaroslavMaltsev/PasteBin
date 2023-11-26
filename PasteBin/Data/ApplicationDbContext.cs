using Microsoft.EntityFrameworkCore;
using PasteBin.Model;

namespace PasteBin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Past> Pasts { get; set; }
    }
}
