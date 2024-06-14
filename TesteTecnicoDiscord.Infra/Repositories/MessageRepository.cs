using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class MessageRepository(AppDbContext context) : EFRepository<Message>(context), IMessageRepository
{
    public async Task<List<Message>> GetAllByChannelId(Guid channelId)
    {
        return await context.Messages.Where(x => x.ChannelId == channelId).AsNoTracking().ToListAsync();
    }

    public async Task<int> GetMessageCountFromUser(Guid userId)
    {
        return await context.Messages.CountAsync(x => x.UserId == userId);
    }
}