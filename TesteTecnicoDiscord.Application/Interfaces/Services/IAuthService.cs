using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> Register(CreateUserDto request);
    Task<string> Login(LoginUserDto request);
}