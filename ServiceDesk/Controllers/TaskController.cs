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
    
    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult Index() => View(_taskRepository.GetAllTasks());
    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult TaskDetails() => View();

    [Authorize(Roles = "Заказчик")]
    public IActionResult Create() => View();
    
    [Authorize(Roles = "Заказчик")]
    [HttpPost]
    public IActionResult Create(TaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            _taskRepository.AddNewTask(model);
            RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpGet]
    [Authorize(Roles = "Заказчик, Исполнитель")]
    public IActionResult Completed() => View();
    // [Authorize(Roles = "Заказчик")]
    // [HttpPost]
    // public IActionResult ChangeDeadline()
    // {
    //     
    //     return RedirectToAction("Index");
    // }

    // [Authorize(Roles = "Заказчик, Исполнитель")]
    // [HttpPost]
    // public IActionResult AddComment()
    // {
    //     return View();
    // }

    [Authorize(Roles = "Исполнитель")]
    [HttpPost]
    public IActionResult ChangeExecutor()
    {
        return View();
    }
    
    [Authorize(Roles = "Исполнитель")]
    [HttpPost]
    public IActionResult ChangeStatus()
    {
        return View();
    }

    public IActionResult Filter(string filter)
    {
        var model = _taskRepository.FilterTasks(filter);
        return View(model);
    }
    public IActionResult CompleteTask()
    {
        try
        {
            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            return NotFound(exception.Message);
        }
    }
}