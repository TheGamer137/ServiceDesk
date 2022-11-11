using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceDesk.Models.ViewModels;

public class TaskFilterViewModel
{
    public IEnumerable<Task> Tasks { get; set; }
    public SelectList SelectStatus { get; set; }
    public SelectList SelectExecutor { get; set; }
    public SelectList SelectDeadline { get; set; }
}