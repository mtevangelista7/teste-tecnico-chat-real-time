using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> GetUserById(Guid id)
    {
        return await userRepository.GetById(id);
    }
}