using Microsoft.AspNetCore.Identity;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;

namespace ServiceDesk.Data.Repositories;

public interface IUserRepository
{
    List<UserViewModel> GetAllUsers();
    void ChangeRole(UserViewModel model);
    void DeleteUser(Guid userId);
}