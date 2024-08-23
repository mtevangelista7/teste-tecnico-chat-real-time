using FluentResults;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<Result<User?>> GetByUsername(string username);
}