using TeamTaskTracker.Models;

namespace TeamTaskTracker.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
