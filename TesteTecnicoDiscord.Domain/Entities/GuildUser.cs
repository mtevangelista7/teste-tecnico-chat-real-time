namespace TesteTecnicoDiscord.Domain.Entities;

public class GuildUser
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid GuildId { get; set; }
    public Guild Guild { get; set; }
}
