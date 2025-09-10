using Dtos.UserLoginDto;
using Dtos.UserRegisterDto;
using Entities.User;
using Interfaces.IUserAuth;

namespace Services.UserService
{
    public class UserService : IUserAuth
    {
        private readonly List<User> _users = new List<User>();

        public bool Register(UserRegisterDto dto)
        {
            if (_users.Any(u => u.Email == dto.Email))
                return false;
            _users.Add(new User
            {
                FirstName = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            });
            return true;
        }
        public User? Login(UserLoginDto dto)
        {
            return _users.FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);
        }
        public List<User> GetUsers()
        {
            return _users;
        }
    }
}