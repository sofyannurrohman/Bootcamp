using BootcampDay23AdvancedReading.Models;
using BootcampDay23AdvancedReading.Repository;
using BootcampDay23AdvancedReading.Services;
using Microsoft.Extensions.Logging;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;
    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public Task<IEnumerable<TaskItem>> GetTasksAsync()
    {
        _logger.LogInformation("Get All Task");
        return _taskRepository.GetAllAsync();
    }
    public async Task AddTaskAsync(string title)
    {
        _logger.LogInformation("Adding new task {Title}", title);
        var task = new TaskItem(title);
        await _taskRepository.AddAsync(task);
    }
    public async Task CompleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");
        if (task == null)
        {
            _logger.LogWarning("Attempted to complete non-existent task {TaskId}", id);
            return;
        }

        task.Completed();
        await _taskRepository.UpdateAsync(task);
         _logger.LogInformation("Task {TaskId} marked as completed", id);
    }
}