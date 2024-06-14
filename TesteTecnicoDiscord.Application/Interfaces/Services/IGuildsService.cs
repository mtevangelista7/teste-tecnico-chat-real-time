using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IGuildsService : IGenericService<Guild>
{
    Task<Guild> CreateNewGuild(CreateGuildDto guild);
}