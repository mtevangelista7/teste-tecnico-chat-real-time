using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        throw new NotImplementedException();
    }
}