using FluentResults;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IGuildsRepository : IRepository<Guild>
{
    Task<Result<Guild>> CreateNewGuild(Guild guild);
    Task<Result> AddUserToGuild(Guid userId, Guid guildId);
    Task<Result<int>> GetGuildCountFromUser(Guid userId);
}