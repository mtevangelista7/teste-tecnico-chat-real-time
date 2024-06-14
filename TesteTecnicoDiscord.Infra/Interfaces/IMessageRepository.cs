using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    Task<List<Message>> GetAllByChannelId(Guid id);
    Task<int> GetMessageCountFromUser(Guid userId);
    new Task<Message> Add(Message message);
}