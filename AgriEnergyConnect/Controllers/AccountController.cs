using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        /*
Title: ASP.NET Core MVC Authentication without Identity
Author: Code Maze
Date: 14 July 2023
Code version: 1
Availability: https://code-maze.com/aspnetcore-authentication-without-identity/
*/

        [HttpPost]
        public IActionResult Register(string username, string password, int roleId)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("", "Username already exists.");
                ViewBag.Roles = _context.Roles.ToList();
                return View();
            }

            var user = new User
            {
                Username = username,
                Password = password,
                RoleId = roleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        /*
Title: Working with Sessions in ASP.NET Core
Author: Microsoft Learn
Date: 2023
Code version: 6
Availability: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0
*/

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role.RoleName);
                HttpContext.Session.SetString("UserId", user.UserId.ToString());

                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
