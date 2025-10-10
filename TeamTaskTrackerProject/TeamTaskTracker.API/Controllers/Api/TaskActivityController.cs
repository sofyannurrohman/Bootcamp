using Microsoft.AspNetCore.Mvc;
using TeamTaskTracker.Services;
using TeamTaskTracker.DTOs;

namespace TeamTaskTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskActivityController : ControllerBase
{
    private readonly ITaskActivityService _taskActivityService;
    public TaskActivityController(ITaskActivityService taskActivityService)
    {
        _taskActivityService = taskActivityService;
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetByTaskId(int taskId)
    {
        var activities = await _taskActivityService.GetByTaskIdAsync(taskId);
        return Ok(activities);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskActivityDto dto)
    {
        var created = await _taskActivityService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByTaskId), new { taskId = created.TaskProjectId }, created);
    }
}
