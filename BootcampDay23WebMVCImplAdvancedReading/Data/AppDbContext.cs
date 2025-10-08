using BootcampDay23AdvancedReading.Models;
using Microsoft.EntityFrameworkCore;

namespace BootcampDay23AdvancedReading.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
