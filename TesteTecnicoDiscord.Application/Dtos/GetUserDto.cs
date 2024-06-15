namespace TesteTecnicoDiscord.Application.Dtos;

public class GetUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Username { get; set; }

    public DateTime BirthDate { get; set; }
    public DateTime DateCreated { get; set; }
}