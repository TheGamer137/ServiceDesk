using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Models;
using ServiceDesk.Models.ViewModels;
using Task = ServiceDesk.Models.Task;

namespace ServiceDesk.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult Index(int? page)=>View(_taskRepository.GetAllTasks(page));
    
    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult Index(string currentFilter, string searchString, int? page)=>View( _taskRepository.SearchTasks(currentFilter, searchString, page));
    
    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult TaskDetails() => View();

    [HttpGet]
    [Authorize(Roles = "Заказчик")]
    public IActionResult Create(int clientId)
    {
        var model = _taskRepository.FindCurrentClient(clientId);
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Заказчик")]
    public IActionResult Create(CreateTaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            _taskRepository.AddNewTask(model);
            RedirectToAction("Index", "Task");
        }
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult ArchiveTasks()=>RedirectToAction("Index", "Archive");
    
    [HttpPut]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Заказчик")]
    public IActionResult ChangeDeadline(DisplayTaskViewModel model)
    {
        _taskRepository.ChangeTaskDeadline(model);
        return RedirectToAction("Index");
    }
    
    [HttpPut]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult AddComment(DisplayTaskViewModel model)
    {
        _taskRepository.AddComment(model);
        return RedirectToAction("Index");
    }
    
    [HttpPut]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Исполнитель")]
    public IActionResult ChangeExecutor(DisplayTaskViewModel model)
    {
        _taskRepository.ChangeTaskExecutor(model);
        return RedirectToAction("Index");
    }
    
    [HttpPut]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Исполнитель")]
    public IActionResult ChangeStatus(DisplayTaskViewModel model)
    {
        _taskRepository.ChangeTaskDeadline(model);
        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public IActionResult CompleteTask()
    {
        try
        {
            return RedirectToAction("Index", "Task");
        }
        catch (Exception exception)
        {
            return NotFound(exception.Message);
        }
    }
}