using TeamTaskTracker.Models;

namespace TeamTaskTracker.Repositories;

public interface ITaskProjectRepository : IRepository<TaskProject>
{
    Task<IEnumerable<TaskProject>> GetByProjectIdAsync(int projectId);
}
