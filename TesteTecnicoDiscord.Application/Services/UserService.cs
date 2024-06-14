using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class UserService(IRepository<User> repository) : GenericService<User>(repository), IUserService
{
}