using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;

namespace ServiceDesk.Controllers;

[Authorize(Roles = "admin")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
 
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
 
    [HttpGet]
    public IActionResult Index() => View(_userRepository.GetAllUsers());
    
    [HttpPost]
    public IActionResult ChangeUserRole(UserViewModel model)
    {
        if (ModelState.IsValid)
            _userRepository.ChangeRole(model);
        return RedirectToAction("Index", "User");
    }
    
    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            return NotFound(exception.Message);
        }
    }
}