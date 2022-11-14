using PagedList;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;
using Task = ServiceDesk.Models.Task;

namespace ServiceDesk.Data.Repositories;

public interface ITaskRepository
{
    IPagedList<DisplayTaskViewModel> GetAllTasks(int? page);
    IPagedList<DisplayTaskViewModel> SearchTasks(string currentFilter, string searchString, int? page);
    Task AddNewTask(CreateTaskViewModel model);
    CreateTaskViewModel FindCurrentClient(int clientId);
    void TakeTask(DisplayTaskViewModel model);
    void AddComment(DisplayTaskViewModel model);
    void ChangeTaskExecutor(DisplayTaskViewModel model);
    void ChangeTaskDeadline(DisplayTaskViewModel model);
    bool CompleteTask(int id);
}