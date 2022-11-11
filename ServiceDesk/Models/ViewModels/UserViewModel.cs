using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Models.ViewModels;

public class UserViewModel
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public int SelectedRole { get; set; }
    public int UserRoleId { get; set; }
    public int CurrentRoleId { get; set; }
    public List<Role> UserRoles { get; set; }
}