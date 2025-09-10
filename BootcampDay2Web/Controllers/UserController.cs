using Dtos.UserRegisterDto;
using Entities.User;
using Interfaces.IUserAuth;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Controllers.UserController
{
    public class UserController : Controller
    {
        private readonly IUserAuth _userService;
        public UserController(IUserAuth userService)
        {
            _userService = userService;
        }
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
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Email Already Registered ");
                }
            }
            return View("Login");
        }
        public IActionResult Welcome(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}