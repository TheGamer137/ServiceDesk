using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Models;

public class Client:User
{
    [Key]
    public int Id { get; set; }
    public string ClientName { get; set; }
}