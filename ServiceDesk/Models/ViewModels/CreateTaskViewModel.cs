namespace ServiceDesk.Models.ViewModels;

public class CreateTaskViewModel
{
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public Client Client { get; set; }
    public DateTime Deadline { get; set; }
}