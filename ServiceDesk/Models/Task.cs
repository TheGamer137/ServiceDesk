using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models;

public class Task
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Client Client { get; set; }
    public Executor Executor { get; set; }
    public Status Status { get; set; }
    public DateTime Deadline { get; set; }
    public string Comment { get; set; }
}

