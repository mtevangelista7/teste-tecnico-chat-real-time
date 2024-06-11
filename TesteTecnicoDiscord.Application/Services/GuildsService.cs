using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class GuildsService(IGuildsRepository guildsRepository, IUserRepository userRepository) : IGuildsService
{
    public async Task<List<Guild>> GetAll()
    {
        return await guildsRepository.GetAll();
    }

    public async Task<Guild> CreateNewGuild(CreateGuildDto guildRequest)
    {
        var user = await userRepository.GetById(guildRequest.OwnerId);

        if (user is null)
            return null!;

        var guild = new Guild()
        {
            Name = guildRequest.Name,
            OwnerUser = user
        };

        var newGuild = await guildsRepository.Add(guild);
        return newGuild;
    }
}