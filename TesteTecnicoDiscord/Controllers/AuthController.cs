using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] CreateUserDto request)
    {
        try
        {
            var result = await authService.Register(request);

            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors.First().Message);

            return string.IsNullOrWhiteSpace(result.Value) ? Results.BadRequest() : Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginUserDto request)
    {
        try
        {
            var result = await authService.Login(request);

            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors.First().Message);

            return string.IsNullOrWhiteSpace(result.Value) ? Results.BadRequest() : Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}