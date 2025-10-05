using ECommerce.API.DTOs;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) { _auth = auth; }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var created = await _auth.RegisterAsync(dto);
        return CreatedAtAction(nameof(Register), new { id = created.Id }, new { created.Username, created.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var res = await _auth.AuthenticateAsync(dto.Username, dto.Password);
        if (!res.Success) return Unauthorized(res);
        return Ok(res);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
    {
        var res = await _auth.RefreshTokenAsync(dto.RefreshToken);
        if (!res.Success) return Unauthorized(res);
        return Ok(res);
    }
}
