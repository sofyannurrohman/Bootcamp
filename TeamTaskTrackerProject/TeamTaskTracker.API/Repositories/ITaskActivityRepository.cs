using TeamTaskTracker.Models;

namespace TeamTaskTracker.Repositories;

public interface ITaskActivityRepository : IRepository<TaskActivity>
{
    Task<IEnumerable<TaskActivity>> GetByTaskIdAsync(int taskId);
}
