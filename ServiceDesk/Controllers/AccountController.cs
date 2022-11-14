using System.Net.Cache;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;
using Task = System.Threading.Tasks.Task;

namespace ServiceDesk.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public AccountController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Register() => View();
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                user = new User 
                { 
                    Email = model.Email, 
                    FirstName = model.FirstName, 
                    LastName = model.LastName, 
                    Password = model.Password 
                };
                Role role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "Заказчик");
                if (role != null)
                    user.Role = role;
                Client client = new Client
                {
                    ClientName = model.FirstName + " " + model.LastName
                };
                _dbContext.Clients.Add(client);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
 
                await Authenticate(user); // аутентификация
 
                return RedirectToAction("Index", "Task");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _dbContext.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user); // аутентификация
                if (user.Role == await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "admin"))
                {
                    return RedirectToAction("Index", "User");
                }
                return RedirectToAction("Index", "Task");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }
 
    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName)
        };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        // аутентификационные куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
 
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}