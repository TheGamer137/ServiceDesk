using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceDesk.Models.ViewModels;

public class DisplayTaskViewModel
{
    public int TaskId { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public Executor? CurrentExecutor { get; set; }
    public List<Executor> Executors { get; set; }
    public DateTime Deadline { get; set; }
    public List<Status> StatusList { get; set; }
    public string Comment { get; set; }
    public string CurrentFilter { get; set; }
    public SelectList SelectStatus { get; set; }
    public SelectList SelectExecutor { get; set; }
    public SelectList SelectDeadline { get; set; }
}