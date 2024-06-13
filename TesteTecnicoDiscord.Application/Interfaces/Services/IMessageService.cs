using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IMessageService
{
    Task<Message> CreateNewMessage(Message message);
    Task<List<Message>> GetMessagesFromChannel(Guid channelId);
}