namespace ServiceDesk.Models.ViewModels;

public class TaskViewModel
{
    public int TaskId { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public Guid ClientId { get; set; }
    public Guid ExecutorId { get; set; }
    public DateTime Deadline { get; set; }
    public int StatusId { get; set; }
    public string Comment { get; set; }
}