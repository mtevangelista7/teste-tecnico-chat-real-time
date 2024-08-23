using FluentResults;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IChannelRepository : IRepository<Channel>
{
    Task<Result<List<Channel>>> GetAllChannelsById(Guid guildId);
    Task<Result> AddUserToChannel(Guid userId, Guid channelId);
}