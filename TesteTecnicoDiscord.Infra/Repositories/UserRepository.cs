using FluentResults;
using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories;

public class UserRepository(AppDbContext context) : EfRepository<User>(context), IUserRepository
{
    public async Task<Result<User?>> GetByUsername(string username)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
        return Result.Ok(user);
    }
}