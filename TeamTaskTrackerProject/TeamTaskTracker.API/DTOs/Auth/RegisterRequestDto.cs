using System.ComponentModel.DataAnnotations;

namespace TeamTaskTracker.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(6)]
    public string Password { get; set; } = null!;
}
