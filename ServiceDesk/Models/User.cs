using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Models;

public class User : IdentityUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? RoleId { get; set; }
    public Role Role { get; set; }
    // public bool IsAuthenticated { get; set; } = false;
}