
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IChannelService
{
    Task<List<Channel>> GetAllChannelsById(Guid guildId);
    Task<Channel> CreateNewChannel(CreateChannelDto channelDto);
    Task<Channel> GetChannelById(Guid channelId);
}