using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto request)
    {
        try
        {
            string token = await authService.Register(request);
            return string.IsNullOrWhiteSpace(token) ? BadRequest() : Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        try
        {
            string token = await authService.Login(request);
            return string.IsNullOrWhiteSpace(token) ? BadRequest() : Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}