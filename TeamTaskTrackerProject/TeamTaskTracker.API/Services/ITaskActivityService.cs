using TeamTaskTracker.DTOs;
namespace TeamTaskTracker.Services;

public interface ITaskActivityService
{
    Task<IEnumerable<TaskActivityDto>> GetByTaskIdAsync(int taskId);
    Task<TaskActivityDto> CreateAsync(TaskActivityDto dto);
}
