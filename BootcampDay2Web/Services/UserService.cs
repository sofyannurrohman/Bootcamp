using Dtos.UserLoginDto;
using Dtos.UserRegisterDto;
using Entities.User;
using Interfaces.IUserAuth;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Data;

namespace Services.UserService
{
    public class UserService : IUserAuth
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool Register(UserRegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return false;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? Login(UserLoginDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);
            return _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.Password == hashedPassword);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
