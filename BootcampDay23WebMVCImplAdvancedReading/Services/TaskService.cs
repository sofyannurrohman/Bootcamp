using BootcampDay23AdvancedReading.Models;
using BootcampDay23AdvancedReading.Repository;
using BootcampDay23AdvancedReading.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<IEnumerable<TaskItem>> GetTasksAsync()
    {
        return _taskRepository.GetAllAsync();
    }
    public async Task AddTaskAsync(string title)
    {
        var task = new TaskItem(title);
        await _taskRepository.AddAsync(task);
    }
    public async Task CompleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");
        task.Completed();
        await _taskRepository.UpdateAsync(task);
    }
}