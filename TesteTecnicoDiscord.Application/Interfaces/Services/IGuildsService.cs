using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IGuildsService
{
    Task<List<Guild>> GetAll();
    Task<Guild> CreateNewGuild(CreateGuildDto guild);
    Task DeleteGuild(Guid guildId);
    Task<Guild> GetById(Guid guildId);
}