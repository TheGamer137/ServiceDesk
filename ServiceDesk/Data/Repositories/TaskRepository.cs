using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;
using Task = ServiceDesk.Models.Task;

namespace ServiceDesk.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TaskRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
    public ICollection<Models.Task> GetAllTasks() => _dbContext.Tasks.ToList();
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

    public async Task<Task> AddNewTask(TaskViewModel model)
    {
        var client = _dbContext.Users.FirstOrDefault(u => u.Id == model.ClientId);
        Task task = new Task();
        {
            task.Title = model.TaskTitle;
            task.Client.Id = client.Id;
            task.Description = model.TaskDescription;
            task.Status.StatusId = 1;
            task.Deadline = model.Deadline;
        }
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<Task> EditTask(TaskViewModel model)
    {
        var executor = _dbContext.Users.FirstOrDefault(u => u.Id == model.ExecutorId);
        Task task = new Task();
        {
            task.Id = model.TaskId;
            task.Executor.ExecutorName = executor.FirstName + " " + executor.LastName;
            task.Status.StatusId = model.StatusId;
            task.Deadline = model.Deadline;
            task.Comment = task.Comment;
        }
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();
        return task;
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