using BootcampDay23AdvancedReading.Models;

namespace BootcampDay23AdvancedReading.Services;
public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync();
    Task AddTaskAsync(string title);
    Task CompleteTaskAsync(int id);
}