using Dtos.UserLoginDto;
using Dtos.UserRegisterDto;
using Entities.User;
using Microsoft.AspNetCore.Components.Forms;

namespace Interfaces.IUserAuth
{
    public interface IUserAuth
    {

        bool Register(UserRegisterDto dto);
        User? Login(UserLoginDto dto);
        List<User> GetUsers();
    }
}