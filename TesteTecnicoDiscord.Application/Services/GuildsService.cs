using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class GuildsService(
    IRepository<Guild?> repository,
    IUserRepository userRepository,
    IGuildsRepository guildsRepository) : GenericService<Guild>(repository), IGuildsService
{
    public async Task<Guild> CreateNewGuild(CreateGuildDto guildRequest)
    {
        var user = await userRepository.GetById(guildRequest.OwnerId);

        if (user is null)
            return null!;

        var guild = new Guild
        {
            Name = guildRequest.Name,
            MembersCount = 1,
            MessagesCount = 0,
            OwnerUser = user
        };

        // TODO: é preciso verificar se não deu falha aqui
        var newGuild = await guildsRepository.CreateNewGuild(guild);
        return newGuild.Value;
    }

    public async Task AddUserToGuild(Guid userId, Guid guildId)
    {
        await guildsRepository.AddUserToGuild(userId, guildId);
    }

    public async Task<int> GetGuildCountFromUser(Guid userId)
    {
        // TODO: é preciso verificar se não deu falha aqui
        return (await guildsRepository.GetGuildCountFromUser(userId)).Value;
    }
}