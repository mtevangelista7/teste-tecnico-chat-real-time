using TesteTecnicoDiscord.Domain.Entities.Base;

namespace TesteTecnicoDiscord.Domain.Entities;

public class User : EntityBase
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public DateTime BirthDate { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}