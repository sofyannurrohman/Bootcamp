using Microsoft.EntityFrameworkCore;
using Entities.User;

namespace Data
{
    public class AppDbContext : DbContext
    {
        // ✅ This is the ONLY constructor you need
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
