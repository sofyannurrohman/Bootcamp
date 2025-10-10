using AutoMapper;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;
using TeamTaskTracker.Services;
namespace TeamTaskTracker.Services;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repo;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _repo.GetByIdAsync(id);
        return _mapper.Map<ProjectDto?>(project);
    }

    public async Task<ProjectDto> CreateAsync(ProjectDto dto, int userId)
    {
        var project = _mapper.Map<Project>(dto);
        project.UserId = userId;
        await _repo.AddAsync(project);
        await _repo.SaveChangesAsync();
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto?> UpdateAsync(int id, ProjectDto dto)
    {
        var project = await _repo.GetByIdAsync(id);
        if (project == null) return null;

        _mapper.Map(dto, project);
        _repo.Update(project);
        await _repo.SaveChangesAsync();
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _repo.GetByIdAsync(id);
        if (project == null) return false;

        _repo.Delete(project);
        await _repo.SaveChangesAsync();
        return true;
    }
}
