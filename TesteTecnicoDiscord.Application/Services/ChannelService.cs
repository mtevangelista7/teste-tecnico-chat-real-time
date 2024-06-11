using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class ChannelService(IChannelRepository channelRepository, IGuildsRepository guildsRepository) : IChannelService
{
    public async Task<List<Channel>> GetAllChannelsById(Guid guildId)
    {
        return await channelRepository.GetAllChannelsById(guildId);
    }

    public async Task<Channel> CreateNewChannel(CreateChannelDto channelDto)
    {
        var guild = await guildsRepository.GetById(channelDto.GuildId);

        var channel = new Channel
        {
            Name = channelDto.Name,
            Guild = guild
        };

        await channelRepository.Add(channel);

        return channel;
    }
}