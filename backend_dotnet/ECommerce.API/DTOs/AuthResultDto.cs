namespace ECommerce.API.DTOs;

public class AuthResultDto
{
    public bool Success { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string[]? Errors { get; set; }
}
