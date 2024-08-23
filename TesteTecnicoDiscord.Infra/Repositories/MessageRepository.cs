using FluentResults;
using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Domain.Util;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class MessageRepository(AppDbContext context) : EfRepository<Message>(context), IMessageRepository
{
    public async Task<Result<List<Message>>> GetAllByChannelId(Guid channelId)
    {
        return Result.Ok(await context.Messages.Where(x => x.ChannelId == channelId).AsNoTracking().ToListAsync());
    }

    public async Task<Result<int>> GetMessageCountFromUser(Guid userId)
    {
        return Result.Ok(await context.Messages.CountAsync(x => x.UserId == userId));
    }

    public new async Task<Result<Message>> Add(Message message)
    {
        context.Messages.Add(message);

        var guild = await context.Guilds.FindAsync(message.GuildId);

        if (guild == null) return Result.Fail<Message>(string.Format(Messages.Errors.GuildNotFound, message.GuildId));

        var channel = await context.Channels.FindAsync(message.ChannelId);

        if (channel == null)
            return Result.Fail<Message>(string.Format(Messages.Errors.ChannelNotFound, message.ChannelId));

        guild.MessagesCount++;
        channel.Messages.Add(message);

        await context.SaveChangesAsync();
        return Result.Ok(message);
    }
}