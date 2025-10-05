using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = null!; // unique

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!; // unique

    public DateTime? LastLogin { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
