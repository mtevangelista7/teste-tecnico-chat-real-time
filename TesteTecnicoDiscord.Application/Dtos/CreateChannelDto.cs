namespace TesteTecnicoDiscord.Application.Dtos;

public class CreateChannelDto
{
    public string Name { get; set; }
    public Guid GuildId { get; set; }
}