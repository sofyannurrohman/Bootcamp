using TeamTaskTracker.DTOs;
namespace TeamTaskTracker.Services;

public interface ITaskProjectService
{
    Task<IEnumerable<TaskProjectDto>> GetAllAsync();
    Task<TaskProjectDto?> GetByIdAsync(int id);
    Task<TaskProjectDto> CreateAsync(TaskProjectDto dto);
    Task<TaskProjectDto?> UpdateAsync(int id, TaskProjectDto dto);
    Task<bool> DeleteAsync(int id);
}
