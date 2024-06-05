using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Guild> Guilds { get; set; }
}