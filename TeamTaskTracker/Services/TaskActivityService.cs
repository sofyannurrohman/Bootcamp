using AutoMapper;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;
using TeamTaskTracker.Services;

public class TaskActivityService : ITaskActivityService
{
    private readonly ITaskActivityRepository _repo;
    private readonly IMapper _mapper;

    public TaskActivityService(ITaskActivityRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskActivityDto>> GetByTaskIdAsync(int taskId)
    {
        var activities = await _repo.GetByTaskIdAsync(taskId);
        return _mapper.Map<IEnumerable<TaskActivityDto>>(activities);
    }

    public async Task<TaskActivityDto> CreateAsync(TaskActivityDto dto)
    {
        var activity = _mapper.Map<TaskActivity>(dto);
        await _repo.AddAsync(activity);
        await _repo.SaveChangesAsync();
        return _mapper.Map<TaskActivityDto>(activity);
    }
}
