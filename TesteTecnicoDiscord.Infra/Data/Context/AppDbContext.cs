using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interceptors;

namespace TesteTecnicoDiscord.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<GuildUser> GuildUsers { get; set; }
    public DbSet<ChannelUser> ChannelUsers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new GuildSaveChangesInterceptor());
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GuildUser>()
            .HasKey(gu => new { gu.UserId, gu.GuildId });

        modelBuilder.Entity<GuildUser>()
            .HasOne(gu => gu.User)
            .WithMany(u => u.GuildUsers)
            .HasForeignKey(gu => gu.UserId);

        modelBuilder.Entity<GuildUser>()
            .HasOne(gu => gu.Guild)
            .WithMany(g => g.GuildUsers)
            .HasForeignKey(gu => gu.GuildId);

        modelBuilder.Entity<ChannelUser>()
            .HasKey(cu => new { cu.UserId, cu.ChannelId });

        modelBuilder.Entity<ChannelUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.ChannelUsers)
            .HasForeignKey(cu => cu.UserId);

        modelBuilder.Entity<ChannelUser>()
            .HasOne(cu => cu.Channel)
            .WithMany(c => c.ChannelUsers)
            .HasForeignKey(cu => cu.ChannelId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Guild)
            .WithMany(g => g.Messages)
            .HasForeignKey(m => m.GuildId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Channel)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChannelId);
    }
}