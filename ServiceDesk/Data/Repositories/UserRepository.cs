
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
        var users = _dbContext.Users.Where(u=>u.Email!="admin@mail.ru").Include(u=>u.Role);
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
        // _dbContext.Roles.ToList();
        User user = _dbContext.Users.FirstOrDefault(u=>u.Id==model.UserId);
        if(user!=null)
        {
            user.Role.Id = model.SelectedRole;
            if (user.Role.RoleName == "Заказчик")
            {
                var client = new Client
                {
                    ClientName = user.FirstName + " " + user.LastName
                };
                var executor = _dbContext.Executors.Find(client);
                _dbContext.Clients.Add(client);
                _dbContext.Executors.Remove(executor);
            }
            if (user.Role.RoleName == "Исполнитель")
            {
                var executor = new Executor
                {
                    ExecutorName = user.FirstName + " " + user.LastName
                };
                var client = _dbContext.Clients.Find(executor);
                _dbContext.Executors.Add(executor);
                _dbContext.Clients.Remove(client);
            }
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