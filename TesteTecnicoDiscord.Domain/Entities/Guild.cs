using TesteTecnicoDiscord.Domain.Entities.Base;

namespace TesteTecnicoDiscord.Domain.Entities;

public class Guild : EntityBase
{
    public string Name { get; set; }
    public User OwnerUser { get; set; }
    public int MembersCount { get; set; }
    public int MessagesCount { get; set; }
}