using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Controllers;

public class ArchiveController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}