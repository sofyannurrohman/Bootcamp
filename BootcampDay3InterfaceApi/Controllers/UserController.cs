using BootcampDay3InterfaceApi.Models;
using Microsoft.AspNetCore.Mvc;
using BootcampDay3InterfaceApi.Repository;
using BootcampDay3InterfaceApi.Interface;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly ILoggingService _logger;
    private readonly ICacheService _cache;

    public UsersController(IUserRepository repo, ILoggingService logger, ICacheService cache)
    {
        _repo = repo;
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        const string cacheKey = "all-users";

        // Try cache first
        if (_cache.Get(cacheKey) is IEnumerable<User> cachedUsers)
        {
            _logger.Log("Cache hit: returning all users from cache.");
            return Ok(cachedUsers);
        }

        // Fetch from repo if not cached
        var users = await _repo.GetAllAsync();
        _cache.Set(cacheKey, users);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        string cacheKey = $"user-{id}";

        // Try cache
        if (_cache.Get(cacheKey) is User cachedUser)
        {
            _logger.Log($"Cache hit: user {id} found in cache.");
            return Ok(cachedUser);
        }

        // Otherwise fetch from repo
        var user = await _repo.GetByIdAsync(id);
        if (user == null)
        {
            _logger.Log($"User {id} not found.");
            return NotFound();
        }

        _cache.Set(cacheKey, user);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        await _repo.AddAsync(user);

        // Invalidate cache since data changed
        _cache.Set($"user-{user.Id}", user);
        _logger.Log($"User {user.Name} created with ID {user.Id}.");

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);

        // Invalidate cache
        _cache.Set($"user-{id}", null!);
        _logger.Log($"User {id} deleted.");

        return NoContent();
    }
}
