using MudBlazor;
using Refit;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Client.RefitInterfaces;

public interface IGuildsEndpoints
{
    [Get("/guilds/getGuilds")]
    public Task<List<GetGuildsDto>> GetGuilds();

    [Post("/guilds/create")]
    public Task CreateNewGuild(CreateGuildDto request);

    [Delete("/guilds/{id}")]
    public Task DeleteGuild(Guid id);

    [Get("/guilds/getGuild/{id}")]
    public Task<GetGuildsDto> GetGuild(Guid id);

    [Get("/guilds/getChannels{id}")]
    public Task<List<GetChannelsDto>> GetChannels(Guid id);

    [Post("/guilds/{guildId}/channels")]
    public Task CreateNewChannel(Guid guildId, CreateChannelDto request);

    [Post("/guilds/channels/message")]
    public Task CreateNewMessage(CreateMessageDto createMessageDto);
}