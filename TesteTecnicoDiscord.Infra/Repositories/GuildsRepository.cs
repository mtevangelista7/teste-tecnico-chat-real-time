using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class GuildsRepository(AppDbContext context) : EFRepository<Guild>(context), IGuildsRepository
{
    public async Task<Guild> CreateNewGuild(Guild guild)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        // add new guild
        context.Guilds.Add(guild);
        await context.SaveChangesAsync();

        // add the creator of server
        guild.GuildUsers.Add(new GuildUser { UserId = guild.OwnerUser.Id, GuildId = guild.Id });
        await context.SaveChangesAsync();

        await transaction.CommitAsync();
        return guild;
    }

    public async Task AddUserToGuild(Guid userId, Guid guildId)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        var user = await context.Users.FindAsync(userId);
        var guild = context.Guilds.Include(g => g.GuildUsers).FirstOrDefault(g => g.Id == guildId);

        if (user == null || guild == null)
        {
            throw new Exception("User or Guild not found");
        }

        var isUserInGuild = guild.GuildUsers.Any(gu => gu.UserId == userId);
        
        if (!isUserInGuild)
        {
            guild.GuildUsers.Add(new GuildUser { UserId = user.Id, GuildId = guild.Id, User = user, Guild = guild});
            guild.MembersCount++;

            await context.SaveChangesAsync();
        }
    }

    public async Task<int> GetGuildCountFromUser(Guid userId)
    {
        return await context.Guilds.CountAsync(x => x.OwnerUser.Id == userId);
    }
}