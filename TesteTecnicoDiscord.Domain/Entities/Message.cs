using TesteTecnicoDiscord.Domain.Entities.Base;

namespace TesteTecnicoDiscord.Domain.Entities;

public class Message : EntityBase
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid GuildId { get; set; }
    public Guild Guild { get; set; }
    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; }
}
