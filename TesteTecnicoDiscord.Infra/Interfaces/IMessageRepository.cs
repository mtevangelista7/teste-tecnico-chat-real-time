using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    Task<List<Message>> GetAllByChannelId(Guid Id);
    Task<int> GetMessageCountFromUser(Guid userId);
}