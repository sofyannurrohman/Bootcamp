using TeamTaskTracker.DTOs;
namespace TeamTaskTracker.Services;
public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<ProjectDto> CreateAsync(ProjectDto dto, int userId);
    Task<ProjectDto?> UpdateAsync(int id, ProjectDto dto);
    Task<bool> DeleteAsync(int id);
}
