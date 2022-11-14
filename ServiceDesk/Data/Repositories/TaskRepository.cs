using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;
using Task = ServiceDesk.Models.Task;
using PagedList.Mvc;

namespace ServiceDesk.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TaskRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public IPagedList<DisplayTaskViewModel> GetAllTasks(int? page)
    {
        var tasks = _dbContext.Tasks.ToList();
        List<DisplayTaskViewModel> model = new List<DisplayTaskViewModel>();
        foreach (var task in tasks)
        {
            var taskModel = new DisplayTaskViewModel()
            {
                TaskId = task.Id,
                TaskTitle = task.Title,
                TaskDescription = task.Description,
                Comment = task.Comment,
                Executors = _dbContext.Executors.ToList(),
                StatusList = _dbContext.Statuses.ToList(),
                CurrentExecutor = task.Executor,
                Deadline = task.Deadline
            };
            model.Add(taskModel);
        }

        page = 1;
        int pageSize = 3;
        int pageNumber = (page ?? 1);
        return model.ToPagedList(pageNumber, pageSize);
    }
    
    public IPagedList<DisplayTaskViewModel> SearchTasks(string currentFilter, string searchString, int? page)
    {
        if (searchString != null)
            page = 1;
        else
            searchString = currentFilter;
        var tasks = _dbContext.Tasks.ToList();
        List<DisplayTaskViewModel> model = new List<DisplayTaskViewModel>();
        foreach (var task in tasks)
        {
            var taskModel = new DisplayTaskViewModel()
            {
                TaskId = task.Id,
                TaskTitle = task.Title,
                TaskDescription = task.Description,
                Comment = task.Comment,
                Executors = _dbContext.Executors.ToList(),
                StatusList = _dbContext.Statuses.ToList(),
                CurrentExecutor = task.Executor,
                Deadline = task.Deadline,
                CurrentFilter = searchString,
                SelectStatus = new SelectList(_dbContext.Statuses), 
                SelectExecutor = new SelectList(_dbContext.Executors), 
                SelectDeadline = new SelectList(_dbContext.Tasks)
            };
            model.Add(taskModel);
        }
        if (!String.IsNullOrEmpty(searchString))
        {
            _dbContext.Tasks.Where(t => t.Status.StatusName == searchString);
            _dbContext.Tasks.Where(t => t.Executor.ExecutorName == searchString);
            _dbContext.Tasks.Where(s => s.Title!.Contains(searchString));
            _dbContext.Tasks.Where(t => t.Description!.Contains(searchString));
            try
            {
                DateTime date = DateTime.Parse(currentFilter);
                _dbContext.Tasks.Where(t => t.Deadline == date);
            }
            catch (FormatException e)
            {
                _dbContext.Tasks.Where(t => t.Comment.Contains(currentFilter));
            }
        }
        int pageSize = 3;
        int pageNumber = (page ?? 1);
        return model.ToPagedList(pageNumber, pageSize);
    }

    public Task AddNewTask(CreateTaskViewModel model)
    {
        Task task = new Task();
        {
            task.Title = model.TaskTitle;
            task.Description = model.TaskDescription;
            task.Status.StatusId = 1;
            task.Deadline = model.Deadline;
            task.Client = model.Client;
        }
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChangesAsync();
        return task;
    }

    public CreateTaskViewModel FindCurrentClient(int clientId)
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        var model = new CreateTaskViewModel
        {
            Client = client
        };
        return model;
    }

    public void TakeTask(DisplayTaskViewModel model)
    {
        Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == model.TaskId);
        {
            if (task != null) 
                task.Status.StatusId = 2;
        }
        _dbContext.Tasks.Entry(task).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void AddComment(DisplayTaskViewModel model)
    {
        Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == model.TaskId);
        {
            if (task != null)
            {
                task.Comment = model.Comment;
            }
        }
        _dbContext.Tasks.Entry(task).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void ChangeTaskExecutor(DisplayTaskViewModel model)
    {
        Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == model.TaskId);
        {
            if (task != null)
            {
                task.Executor.Id = model.CurrentExecutor.Id;
                task.Executor.ExecutorName = model.CurrentExecutor.ExecutorName;
            }
        }
        _dbContext.Tasks.Entry(task).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void ChangeTaskDeadline(DisplayTaskViewModel model)
    {
        Task task = _dbContext.Tasks.FirstOrDefault(t => t.Id == model.TaskId);
        {
            if (task != null)
                task.Deadline = model.Deadline;
        }
        _dbContext.Tasks.Entry(task).State = EntityState.Modified;
        _dbContext.SaveChanges();
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