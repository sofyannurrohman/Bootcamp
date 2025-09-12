using BootcampDay3InterfaceApi.Models;
using Microsoft.AspNetCore.Mvc;
using BootcampDay3InterfaceApi.Repository;
using BootcampDay3InterfaceApi.Dtos;
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
    public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetAll()
    {
        const string cacheKey = "all-users";

        if (_cache.Get(cacheKey) is IEnumerable<User> cachedUsers)
        {
            _logger.Log("Cache hit: returning all users from cache.");
            return Ok(new ApiResponse<IEnumerable<User>>(cachedUsers, "Fetched from cache"));
        }

        var users = await _repo.GetAllAsync();
        _cache.Set(cacheKey, users);

        return Ok(new ApiResponse<IEnumerable<User>>(users, "Fetched from database"));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<User>>> GetById(int id)
    {
        string cacheKey = $"user-{id}";

        if (_cache.Get(cacheKey) is User cachedUser)
        {
            _logger.Log($"Cache hit: user {id} found in cache.");
            return Ok(new ApiResponse<User>(cachedUser, "Fetched from cache"));
        }

        var user = await _repo.GetByIdAsync(id);
        if (user == null)
        {
            _logger.Log($"User {id} not found.");
            return NotFound(new ApiResponse<User>(default!, $"User {id} not found", false));
        }

        _cache.Set(cacheKey, user);
        return Ok(new ApiResponse<User>(user, "Fetched from database"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<User>>> Create(User user)
    {
        await _repo.AddAsync(user);
        _cache.Set($"user-{user.Id}", user);
        _logger.Log($"User {user.Name} created with ID {user.Id}.");

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            new ApiResponse<User>(user, "User created")
        );
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        _cache.Set($"user-{id}", null!);
        _logger.Log($"User {id} deleted.");

        return Ok(new ApiResponse<string>($"User {id} deleted", "Delete successful"));
    }
}
