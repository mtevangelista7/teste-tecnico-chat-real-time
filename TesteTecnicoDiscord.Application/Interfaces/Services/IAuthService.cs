using FluentResults;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<string>> Register(CreateUserDto request);
    Task<Result<string>> Login(LoginUserDto request);
}