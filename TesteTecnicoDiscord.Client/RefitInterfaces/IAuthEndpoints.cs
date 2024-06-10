using Refit;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Client.RefitInterfaces;

public interface IAuthEndpoints
{
    [Post("/auth/register")]
    public Task<string> Register(CreateUserDto request);
    
    [Post("/auth/login")]
    public Task<string> Login(LoginUserDto request);
}