using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class MessageService(
    IRepository<Message> repository,
    IMessageRepository messageRepository,
    IUserRepository userRepository,
    IGuildsRepository guildsRepository,
    IChannelRepository channelRepository)
    : GenericService<Message>(repository), IMessageService
{
    public async Task<List<Message>> GetByChannelId(Guid channelId)
    {
        return await messageRepository.GetAllByChannelId(channelId);
    }

    public async Task<int> GetMessageCountFromUser(Guid userId)
    {
        return await messageRepository.GetMessageCountFromUser(userId);
    }

    public new async Task<Message> Add(CreateMessageDto messageDto)
    {
        var user = await userRepository.GetById(messageDto.UserId);
        var guild = await guildsRepository.GetById(messageDto.GuildId);
        var channel = await channelRepository.GetById(messageDto.ChannelId);

        if (user == null || guild == null || channel == null)
        {
            throw new Exception("User, Guild, or Channel not found");
        }

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

        message = await messageRepository.Add(message);
        return message;
    }
}