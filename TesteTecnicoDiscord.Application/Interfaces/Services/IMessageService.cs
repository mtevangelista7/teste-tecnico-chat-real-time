using FluentResults;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IMessageService : IGenericService<Message>
{
    Task<Result<List<Message>>> GetByChannelId(Guid channelId);
    Task<Result<int>> GetMessageCountFromUser(Guid userId);
     Task<Result<Message>> Add(CreateMessageDto messageDto);
}