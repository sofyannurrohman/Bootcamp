using AutoMapper;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;
using TeamTaskTracker.Services;

public class TaskProjectService : ITaskProjectService
{
    private readonly ITaskProjectRepository _repo;
    private readonly IMapper _mapper;

    public TaskProjectService(ITaskProjectRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskProjectDto>> GetAllAsync()
    {
        var tasks = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskProjectDto>>(tasks);
    }

    public async Task<TaskProjectDto?> GetByIdAsync(int id)
    {
        var task = await _repo.GetByIdAsync(id);
        return _mapper.Map<TaskProjectDto?>(task);
    }

    public async Task<TaskProjectDto> CreateAsync(TaskProjectDto dto)
    {
        var task = _mapper.Map<TaskProject>(dto);
        await _repo.AddAsync(task);
        await _repo.SaveChangesAsync();
        return _mapper.Map<TaskProjectDto>(task);
    }

    public async Task<TaskProjectDto?> UpdateAsync(int id, TaskProjectDto dto)
    {
        var task = await _repo.GetByIdAsync(id);
        if (task == null) return null;

        _mapper.Map(dto, task);
        _repo.Update(task);
        await _repo.SaveChangesAsync();
        return _mapper.Map<TaskProjectDto>(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _repo.GetByIdAsync(id);
        if (task == null) return false;

        _repo.Delete(task);
        await _repo.SaveChangesAsync();
        return true;
    }
}
