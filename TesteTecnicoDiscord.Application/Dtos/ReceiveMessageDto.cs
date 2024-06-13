namespace TesteTecnicoDiscord.Application.Dtos;

public class ReceiveMessageDto
{
    public Guid Id { get; set; }
    public string OwnerUsername { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}