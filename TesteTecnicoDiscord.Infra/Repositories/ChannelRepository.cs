using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class ChannelRepository(AppDbContext context) : EFRepository<Channel>(context), IChannelRepository
{
    public async Task<List<Channel>> GetAllChannelsById(Guid guildId)
    {
        return await context.Channels.Where(x => x.GuildId == guildId).AsNoTracking().ToListAsync();
    }
}