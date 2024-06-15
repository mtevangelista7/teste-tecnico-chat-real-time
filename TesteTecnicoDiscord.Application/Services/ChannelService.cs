using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class ChannelService(
    IRepository<Channel> repository,
    IChannelRepository channelRepository,
    IGuildsRepository guildsRepository) : GenericService<Channel>(repository), IChannelService
{
    public async Task<List<Channel>> GetAllChannelsById(Guid guildId)
    {
        return await channelRepository.GetAllChannelsById(guildId);
    }

    public async Task<Channel> CreateNewChannel(CreateChannelDto channelDto)
    {
        Console.WriteLine(channelDto.GuildId);
        var guild = await guildsRepository.GetById(channelDto.GuildId);

        if (guild is null)
            return null;

        var channel = new Channel
        {
            Name = channelDto.Name,
            Guild = guild
        };

        await channelRepository.Add(channel);
        return channel;
    }

    public async Task AddUserToChannel(Guid userId, Guid channelId)
    {
        await channelRepository.AddUserToChannel(userId, channelId);
    }
}