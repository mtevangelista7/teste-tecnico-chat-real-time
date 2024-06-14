using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IGuildsRepository : IRepository<Guild>
{
    Task<Guild> CreateNewGuild(Guild guild);
    Task AddUserToGuild(Guid userId, Guid guildId);
    Task<int> GetGuildCountFromUser(Guid userId);
}