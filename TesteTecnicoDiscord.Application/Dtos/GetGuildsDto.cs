namespace TesteTecnicoDiscord.Application.Dtos;

public class GetGuildsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CreateUserDto OwnerUser { get; set; }
    public int MembersCount { get; set; }
    public int MessagesCount { get; set; }
}