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

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return false;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginAsync(UserLoginDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == hashedPassword);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
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
