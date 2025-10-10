using TeamTaskTracker.DTOs;
using TeamTaskTracker.DTOs.User;

namespace TeamTaskTracker.Services;
public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserDto dto);
    Task<UserDto?> UpdateAsync(int id, UserDto dto);
    Task<bool> DeleteAsync(int id);
}
