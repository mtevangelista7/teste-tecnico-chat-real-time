using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class MessageService(IMessageRepository messageRepository) : IMessageService
{
    public async Task<Message> CreateNewMessage(Message message)
    {
        return await messageRepository.Add(message);
    }

    public async Task<List<Message>> GetMessagesFromChannel(Guid channelId)
    {
        return await messageRepository.GetAllByChannelId(channelId);
    }
}