using FluentResults;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    Task<Result<List<Message>>> GetAllByChannelId(Guid id);
    Task<Result<int>> GetMessageCountFromUser(Guid userId);
    new Task<Result<Message>> Add(Message message);
}