using ServiceDesk.Models.ViewModels;

namespace ServiceDesk.Data.Repositories;

public interface ITaskRepository
{
    ICollection<TaskViewModel> GetAllTasks();
    Models.Task GetTaskByTitle(string taskTitle);
    Models.Task GetTaskByDescription(string taskTitle);
    TaskFilterViewModel FilterTasks(string searchString);
    void AddNewTask(TaskViewModel model);
    void TakeTask(TaskViewModel model);
    void ChangeTaskExecutor(TaskViewModel model);
    void ChangeTaskDeadline(TaskViewModel model);
    bool CompleteTask(int id);
}