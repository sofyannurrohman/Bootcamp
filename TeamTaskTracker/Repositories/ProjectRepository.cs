using Microsoft.EntityFrameworkCore;
using TeamTaskTracker.Data;
using TeamTaskTracker.Models;

namespace TeamTaskTracker.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
    {
        return await _context.Projects
            .Where(p => p.EndDate >= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
    {
        return await _context.Projects
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
}
