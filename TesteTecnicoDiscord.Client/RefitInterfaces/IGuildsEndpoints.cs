using Refit;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Client.RefitInterfaces;

public interface IGuildsEndpoints
{
    [Get("/guilds/getGuilds")]
    public Task<List<GetGuildsDto>> GetGuilds();
}