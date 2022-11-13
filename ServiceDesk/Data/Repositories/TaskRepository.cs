using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models.ViewModels;
using Task = ServiceDesk.Models.Task;

namespace ServiceDesk.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TaskRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ICollection<TaskViewModel> GetAllTasks()
    {
        var tasks = _dbContext.Tasks.ToList();
        List<TaskViewModel> model = new List<TaskViewModel>();
        foreach (var task in tasks)
        {
            var taskModel = new TaskViewModel()
            {
                TaskId = task.Id,
                TaskTitle = task.Title,
                TaskDescription = task.Description
            };
            model.Add(taskModel);
        }

        return model;
    }
    public Task GetTaskByTitle(string taskTitle)
    {
        throw new NotImplementedException();
    }

    public Task GetTaskByDescription(string taskTitle)
    {
        throw new NotImplementedException();
    }

    public TaskFilterViewModel FilterTasks(string searchString)
    {
        if (!String.IsNullOrEmpty(searchString))
        {
            _dbContext.Tasks.Where(t => t.Status.StatusName == searchString);
            _dbContext.Tasks.Where(t => t.Executor.ExecutorName == searchString);
            try
            {
                DateTime date = DateTime.Parse(searchString);
                _dbContext.Tasks.Where(t => t.Deadline == date);
            }
            catch (FormatException e)
            {
                _dbContext.Tasks.Where(t => t.Comment.Contains(searchString));
            }
        }

        List<Task> tasks = _dbContext.Tasks.ToList();
        TaskFilterViewModel model = new TaskFilterViewModel()
        {
            Tasks = tasks.ToList(),
            SelectStatus = new SelectList(_dbContext.Statuses),
            SelectExecutor = new SelectList(_dbContext.Executors),
            SelectDeadline = new SelectList(_dbContext.Tasks)
        };
        return model;
    }

    public void AddNewTask(TaskViewModel model)
    {
        var client = _dbContext.Users.FirstOrDefault(u => u.FirstName + " " + u.LastName == model.Client.ClientName);
        if (client == null)
        {
            Task task = new Task();
            {
                task.Title = model.TaskTitle;
                task.Description = model.TaskDescription;
                task.Status.StatusId = 1;
                task.Deadline = model.Deadline;
            }
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChangesAsync();
        }
    }

    public void TakeTask(TaskViewModel model)
    {
        throw new NotImplementedException();
    }

    public void ChangeTaskExecutor(TaskViewModel model)
    {
        Task task = new Task();
        {
            task.Executor.Id = model.CurrentExecutor.Id;
            task.Executor.ExecutorName = model.CurrentExecutor.ExecutorName;
        }
        _dbContext.Tasks.Update(task);
        _dbContext.SaveChanges();
    }

    public void ChangeTaskDeadline(TaskViewModel model)
    {
        throw new NotImplementedException();
    }

    public bool CompleteTask(int id)
    {
        bool isCompleted = false;
        Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            isCompleted = true;
            task.Status.StatusId = 3;
            _dbContext.Entry(task).State = EntityState.Modified;
        } 
        return isCompleted;
    }
}