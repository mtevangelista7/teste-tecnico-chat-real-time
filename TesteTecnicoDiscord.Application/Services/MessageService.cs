using System.Text;
using FluentResults;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Domain.Util;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class MessageService(
    IRepository<Message?> repository,
    IMessageRepository messageRepository,
    IUserRepository userRepository,
    IGuildsRepository guildsRepository,
    IChannelRepository channelRepository)
    : GenericService<Message>(repository), IMessageService
{
    public async Task<Result<List<Message>>> GetByChannelId(Guid channelId)
    {
        var result = await messageRepository.GetAllByChannelId(channelId);
        return result;
    }

    public async Task<Result<int>> GetMessageCountFromUser(Guid userId)
    {
        var result = await messageRepository.GetMessageCountFromUser(userId);
        return result;
    }

    public async Task<Result<Message>> Add(CreateMessageDto messageDto)
    {
        var user = await userRepository.GetById(messageDto.UserId);
        var guild = await guildsRepository.GetById(messageDto.GuildId);
        var channel = await channelRepository.GetById(messageDto.ChannelId);

        var sb = new StringBuilder();

        if (user is null) sb.Append("User ");
        if (guild is null) sb.Append("Guild ");
        if (channel is null) sb.Append("Channel ");

        if (sb.Length > 0)
            return Result.Fail<Message>(string.Format(Messages.Errors.ItemNotFound, sb));

        var message = new Message
        {
            Content = messageDto.Content,
            Timestamp = DateTime.Now,
            UserId = messageDto.UserId,
            User = user,
            GuildId = messageDto.GuildId,
            Guild = guild,
            ChannelId = messageDto.ChannelId,
            Channel = channel
        };

        var result = await messageRepository.Add(message);
        return result;
    }
}