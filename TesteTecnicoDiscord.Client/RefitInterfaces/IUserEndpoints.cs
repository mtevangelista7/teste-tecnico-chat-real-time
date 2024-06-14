using Refit;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Client.RefitInterfaces;

public interface IUserEndpoints
{
    [Get("/user")]
    public Task<GetUserDto> GetUser();
}