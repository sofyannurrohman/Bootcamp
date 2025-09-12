using BootcampDay3InterfaceApi.Models;
using BootcampDay3InterfaceApi.Interface;
namespace BootcampDay3InterfaceApi.Repository;
public class InMemoryUserRepository : IUserRepository, ILoggingService, ICacheService
{
    private readonly List<User> _users = new();
    private readonly Dictionary<string, object> _cache = new();

    // === IUserRepository Implementation ===
    public Task<IEnumerable<User>> GetAllAsync()
    {
        Log("Fetching all users...");
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task<User?> GetByIdAsync(int id)
    {
        Log($"Fetching user with ID {id}...");
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task AddAsync(User user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
        Log($"User {user.Name} added with ID {user.Id}");
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            Log($"User with ID {id} deleted.");
        }
        return Task.CompletedTask;
    }

    // === ILoggingService Implementation ===
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }

    // === ICacheService Implementation ===
    public void Set(string key, object value)
    {
        _cache[key] = value;
    }

    public object? Get(string key)
    {
        return _cache.TryGetValue(key, out var value) ? value : null;
    }
}
