using Microsoft.EntityFrameworkCore;
using TeamTaskTracker.Data;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;

public class TaskProjectRepository : Repository<TaskProject>,ITaskProjectRepository
{
    public TaskProjectRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<TaskProject>> GetByProjectIdAsync(int projectId)
    {
        return await _context.Tasks
            .Include(t => t.AssignedTo)
            .Include(t => t.Project)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskProject>> GetByUserIdAsync(int userId)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Where(t => t.AssignedToUserId == userId)
            .ToListAsync();
    }
}
