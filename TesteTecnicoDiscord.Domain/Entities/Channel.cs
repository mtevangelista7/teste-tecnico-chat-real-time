using TesteTecnicoDiscord.Domain.Entities.Base;

namespace TesteTecnicoDiscord.Domain.Entities;

public class Channel : EntityBase
{
    public string Name { get; set; }
    public Guid GuildId { get; set; }
    public Guild Guild { get; set; }
    public ICollection<ChannelUser> ChannelUsers { get; set; } = new List<ChannelUser>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}