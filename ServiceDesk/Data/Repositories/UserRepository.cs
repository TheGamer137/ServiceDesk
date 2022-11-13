
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;

namespace ServiceDesk.Data.Repositories;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public List<UserViewModel> GetAllUsers()
    {

        var users = _dbContext.Users.Where(u=>u.Email!="admin@mail.ru");
        List<UserViewModel> model = new List<UserViewModel>();
        foreach (var user in users)
        {
            var userModel = new UserViewModel()
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.FirstName + " " + user.LastName,
                UserRoles = _dbContext.Roles.Where(r => r.RoleName != "admin").ToList(),
                CurrentRole = user.Role.RoleName
            };
            model.Add(userModel);
        }

        return model;
    }

    public void ChangeRole(UserViewModel model)
    {
        _dbContext.Roles.ToList();
        User user = _dbContext.Users.FirstOrDefault(u=>u.Email==model.Email);
        if(user!=null)
        {
            user.Role.Id = model.SelectedRole;
        }
        _dbContext.Users.Entry(user).State = EntityState.Modified;
    }
    
    public void DeleteUser(Guid userId)
    {
        User user = _dbContext.Users.Find(userId);
        if (user != null)
            _dbContext.Users.Remove(user);
    }
}