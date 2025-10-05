using ECommerce.API.Models;
using ECommerce.API.DTOs;

namespace ECommerce.API.Services;

public interface IAuthService
{
    Task<AuthResultDto> AuthenticateAsync(string username, string password);
    Task<AuthResultDto> RefreshTokenAsync(string refreshToken);
    Task<User> RegisterAsync(RegisterDto dto);
}
