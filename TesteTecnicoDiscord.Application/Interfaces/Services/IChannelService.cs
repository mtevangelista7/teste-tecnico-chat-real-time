
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IChannelService : IGenericService<Channel>
{
    Task<List<Channel>> GetAllChannelsById(Guid guildId);
    Task<Channel> CreateNewChannel(CreateChannelDto channelDto);
}