using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IMessageService: IGenericService<Message>
{
    Task<List<Message>> GetByChannelId(Guid channelId);
    Task<int> GetMessageCountFromUser(Guid userId);
    new Task<Message> Add(CreateMessageDto messageDto);
}