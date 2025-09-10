using Dtos.UserLoginDto;
using Dtos.UserRegisterDto;
using Entities.User;
using Microsoft.AspNetCore.Components.Forms;

namespace Interfaces.IUserAuth
{
    public interface IUserAuth
    {
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<User?> LoginAsync(UserLoginDto dto);
        Task<List<User>> GetUsersAsync();
    }
}