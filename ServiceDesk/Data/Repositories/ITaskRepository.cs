using ServiceDesk.Models.ViewModels;

namespace ServiceDesk.Data.Repositories;

public interface ITaskRepository
{
    ICollection<Models.Task> GetAllTasks();
    Models.Task GetTaskByTitle(string taskTitle);
    Models.Task GetTaskByDescription(string taskTitle);
    TaskFilterViewModel FilterTasks(string searchString);
    Task<Models.Task> AddNewTask(TaskViewModel model);
    Task<Models.Task> EditTask(TaskViewModel model);
    bool CompleteTask(int id);
}