using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IChannelRepository : IRepository<Channel>
{
    Task<List<Channel>> GetAllChannelsById(Guid guildId);
    Task AddUserToChannel(Guid userId, Guid channelId);
}