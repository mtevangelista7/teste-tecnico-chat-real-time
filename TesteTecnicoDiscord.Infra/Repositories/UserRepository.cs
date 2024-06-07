using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class UserRepository(AppDbContext context) : EFRepository<User>(context), IUserRepository
{
}