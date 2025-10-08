using System.Globalization;
using BootcampDay23AdvancedReading.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootcampDay23AdvancedReading.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService service)
    {
        _taskService = service;
    }
    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetTasksAsync();
        return View(tasks);
    }
    [HttpPost]
    public async Task<IActionResult> Create(string title)
    {
        await _taskService.AddTaskAsync(title);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Complete(int id)
    {
        await _taskService.CompleteTaskAsync(id);
        return RedirectToAction("Index");
    }
}