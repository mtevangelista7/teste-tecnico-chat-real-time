using FluentResults;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IChannelService : IGenericService<Channel>
{
    Task<Result<List<Channel>>> GetAllChannelsById(Guid guildId);
    Task<Result<Channel>> CreateNewChannel(CreateChannelDto channelDto);
    Task<Result> AddUserToChannel(Guid userId, Guid channelId);
}