using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class MessageService(IRepository<Message> repository, IMessageRepository messageRepository)
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
}