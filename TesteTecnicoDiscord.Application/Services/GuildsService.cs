using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class GuildsService(
    IRepository<Guild> repository,
    IUserRepository userRepository,
    IGuildsRepository guildsRepository) : GenericService<Guild>(repository), IGuildsService
{
    public async Task<Guild> CreateNewGuild(CreateGuildDto guildRequest)
    {
        var user = await userRepository.GetById(guildRequest.OwnerId);

        if (user is null)
            return null!;

        var guild = new Guild()
        {
            Name = guildRequest.Name,
            MembersCount = 1,
            MessagesCount = 0,
            OwnerUser = user
        };

        var newGuild = await guildsRepository.CreateNewGuild(guild);
        return newGuild;
    }

    public async Task<int> GetGuildCountFromUser(Guid userId)
    {
        return await guildsRepository.GetGuildCountFromUser(userId);
    }
}