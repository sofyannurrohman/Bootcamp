using Dtos.UserLoginDto;
using Dtos.UserRegisterDto;
using Entities.User;
using Interfaces.IUserAuth;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Controllers
{
    public class UserController : Controller
{
    private readonly IUserAuth _userService;
    public UserController(IUserAuth userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserRegisterDto dto)
    {
        if (ModelState.IsValid)
        {
            bool success = _userService.Register(dto);
            if (success)
            {
                return RedirectToAction("Login"); // redirect to Login after register
            }
            else
            {
                ModelState.AddModelError("", "Email Already Registered");
            }
        }
        return View("Register"); // show register view on error
    }

    // ADD THIS
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(UserLoginDto dto)
{
    if (ModelState.IsValid)
    {
        var user = _userService.Login(dto); // returns User? not bool
        if (user != null)
        {
            return RedirectToAction("Welcome", new { name = user.Email });
        }
        else
        {
            ViewBag.Error = "Invalid email or password";
        }
    }
    return View(dto); // return dto to preserve form inputs
}

    public IActionResult Welcome(string name)
    {
        ViewBag.Name = name;
        return View();
    }
}
}