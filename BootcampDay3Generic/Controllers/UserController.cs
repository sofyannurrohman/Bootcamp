using Microsoft.AspNetCore.Mvc;
using BootcampDay3Generic.Models;
using BootcampDay3Generic.Repositories;

namespace BootcampDay3Generic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<User>>> GetUsers()
        {
            var users = _repository.GetAll();
            return Ok(ApiResponse<IEnumerable<User>>.Ok(users));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<User>> GetUser(int id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                return NotFound(ApiResponse<User>.Fail("User not found"));

            return Ok(ApiResponse<User>.Ok(user));
        }

        [HttpPost]
        public ActionResult<ApiResponse<User>> CreateUser(User user)
        {
            _repository.Add(user);
            return Ok(ApiResponse<User>.Ok(user, "User created successfully"));
        }
    }
}
