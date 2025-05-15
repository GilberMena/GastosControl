using GastosControl.Application.Services;
using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using GastosControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosControl.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public AuthController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.AuthenticateAsync(model.Login, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }

            // Guardar usuario en sesión
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FirstName);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identification,FirstName,LastName,Login,Password,BirthDate,Address,Email,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var newUser = await _userService.AuthenticateAsync(user.Login, user.Password);
                if (user == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                HttpContext.Session.SetInt32("UserId", newUser.Id);
                HttpContext.Session.SetString("UserRole", newUser.Role);
                HttpContext.Session.SetString("UserName", newUser.FirstName);

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
