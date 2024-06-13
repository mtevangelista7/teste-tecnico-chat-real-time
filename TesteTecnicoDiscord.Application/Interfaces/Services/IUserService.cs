using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Application.Interfaces.Services;

public interface IUserService
{
    Task<User> GetUserById(Guid id);
}