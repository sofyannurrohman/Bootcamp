using Microsoft.EntityFrameworkCore;
using TeamTaskTracker.Data;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;

public class TaskActivityRepository : Repository<TaskActivity>,ITaskActivityRepository
{
    public TaskActivityRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<TaskActivity>> GetByTaskIdAsync(int taskId)
    {
        return await _context.TaskActivities
            .Where(a => a.TaskId == taskId)
            .OrderByDescending(a => a.Timestamp)
            .ToListAsync();
    }
}
