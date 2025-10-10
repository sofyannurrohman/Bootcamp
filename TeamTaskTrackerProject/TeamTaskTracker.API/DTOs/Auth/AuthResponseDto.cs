namespace TeamTaskTracker.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
}