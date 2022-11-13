using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Models;

public class Executor : User
{
    [Key]
    public int Id { get; set; }
    public string ExecutorName { get; set; }
}