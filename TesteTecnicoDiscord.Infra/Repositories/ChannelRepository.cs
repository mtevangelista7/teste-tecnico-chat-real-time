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

    public async Task AddUserToChannel(Guid userId, Guid channelId)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        var user = await context.Users.FindAsync(userId);
        var channel = context.Channels.Include(c => c.ChannelUsers).FirstOrDefault(c => c.Id == channelId);

        if (user == null || channel == null)
        {
            throw new Exception("User or Channel not found");
        }

        var isUserInChannel = channel.ChannelUsers.Any(cu => cu.UserId == userId);
        if (!isUserInChannel)
        {
            channel.ChannelUsers.Add(new ChannelUser { UserId = user.Id, ChannelId = channel.Id });
            await context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
    }
}