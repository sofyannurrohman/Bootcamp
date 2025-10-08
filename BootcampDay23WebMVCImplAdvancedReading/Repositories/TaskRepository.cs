using BootcampDay23AdvancedReading.Models;
namespace BootcampDay23AdvancedReading.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _task = new();
    public Task<IEnumerable<TaskItem>> GetAllAsync() => Task.FromResult(_task.AsEnumerable());
    public Task<TaskItem?> GetByIdAsync(int id)
    {
        return Task.FromResult(_task.FirstOrDefault(t => t.Id == id));
    }
    public Task AddAsync(TaskItem task)
    {
        task.GetType().GetProperty("Id")!.SetValue(task, _task.Count + 1);
        _task.Add(task);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(TaskItem task)
    {
        return Task.CompletedTask;
    }
}