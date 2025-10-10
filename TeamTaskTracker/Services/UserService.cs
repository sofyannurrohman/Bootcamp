using AutoMapper;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.Models;
using TeamTaskTracker.Repositories;
using TeamTaskTracker.Services;
using BCrypt.Net;
using TeamTaskTracker.DTOs.User;
namespace TeamTaskTracker.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        return _mapper.Map<UserResponseDto?>(user);
    }

    public async Task<UserDto> CreateAsync(UserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, UserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return null;

        _mapper.Map(dto, user);
        _repo.Update(user);
        await _repo.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return false;

        _repo.Delete(user);
        await _repo.SaveChangesAsync();
        return true;
    }
}
