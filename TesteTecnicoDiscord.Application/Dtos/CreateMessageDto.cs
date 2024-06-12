namespace TesteTecnicoDiscord.Application.Dtos;

public class CreateMessageDto
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid UserId { get; set; }
    public Guid GuildId { get; set; }
    public Guid ChannelId { get; set; }
}