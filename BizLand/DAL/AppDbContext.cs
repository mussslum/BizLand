using BizLand.Models;
using Microsoft.EntityFrameworkCore;

namespace BizLand.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<TeamMember>TeamMembers { get; set; }
    }
}
