using ECommerce.API.Data;
using ECommerce.API.DTOs;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.API.Services;

public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly JwtSettings _jwtSettings;

    public AuthService(AppDbContext db, IOptions<JwtSettings> jwtOptions)
    {
        _db = db;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResultDto> AuthenticateAsync(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null) return new AuthResultDto { Success = false, Errors = new[] { "Invalid credentials" } };

        // NOTE: For demo, using plain text compare. In real app use secure hashing (BCrypt).
        if (user.PasswordHash != password) return new AuthResultDto { Success = false, Errors = new[] { "Invalid credentials" } };

        // Update last login
        user.LastLogin = DateTime.UtcNow;
        // generate tokens
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await _db.SaveChangesAsync();

        return new AuthResultDto { Success = true, AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<AuthResultDto> RefreshTokenAsync(string refreshToken)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user == null || user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            return new AuthResultDto { Success = false, Errors = new[] { "Invalid or expired refresh token" } };

        var accessToken = GenerateAccessToken(user);
        var newRefresh = GenerateRefreshToken();
        user.RefreshToken = newRefresh;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await _db.SaveChangesAsync();

        return new AuthResultDto { Success = true, AccessToken = accessToken, RefreshToken = newRefresh };
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password // For demo only - replace with hash
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    private string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
