using Refit;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Client.RefitInterfaces;

public interface IUserEndpoints
{
    [Get("/user")]
    public Task<GetUserDto> GetUser();

    [Get("/user/getMessagesCount/{userId}")]
    public Task<int> GetMessagesCount(Guid userId);

    [Get("/user/getGuildsCount/{userId}")]
    public Task<int> GetGuildCount(Guid userId);

}