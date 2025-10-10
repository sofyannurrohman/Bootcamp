using TeamTaskTracker.Models;

namespace TeamTaskTracker.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetActiveProjectsAsync();
    Task<IEnumerable<Project>> GetByUserIdAsync(int userId);
}
