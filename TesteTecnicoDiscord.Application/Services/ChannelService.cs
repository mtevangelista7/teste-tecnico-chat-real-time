using FluentResults;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Domain.Util;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class ChannelService(
    IRepository<Channel?> repository,
    IChannelRepository channelRepository,
    IGuildsRepository guildsRepository) : GenericService<Channel>(repository), IChannelService
{
    public async Task<Result<List<Channel>>> GetAllChannelsById(Guid guildId)
    {
        var result = await channelRepository.GetAllChannelsById(guildId);
        return !result.IsSuccess ? result : result.Value;
    }

    public async Task<Result<Channel>> CreateNewChannel(CreateChannelDto channelDto)
    {
        var result = await guildsRepository.GetById(channelDto.GuildId);

        if (result is null)
            return Result.Fail<Channel>(string.Format(Messages.Errors.GuildNotFound, channelDto.GuildId));

        var channel = new Channel
        {
            Name = channelDto.Name,
            Guild = result
        };

        await channelRepository.Add(channel);
        return channel;
    }

    public async Task<Result> AddUserToChannel(Guid userId, Guid channelId)
    {
        return await channelRepository.AddUserToChannel(userId, channelId);
    }
}