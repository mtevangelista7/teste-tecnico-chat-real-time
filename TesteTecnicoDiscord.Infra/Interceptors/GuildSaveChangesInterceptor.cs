using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Infra.Interceptors;

public class GuildSaveChangesInterceptor : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        var context = eventData.Context;

        var modifiedGuilds = context.ChangeTracker.Entries<Guild>()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .ToList();

        foreach (var entry in modifiedGuilds)
        {
            var guild = entry.Entity;
            guild.MembersCount = guild.GuildUsers.Count;
            guild.MessagesCount = guild.Messages.Count;
        }

        return base.SavedChanges(eventData, result);
    }
}