using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoDiscord.Application.Dtos;

public class CreateGuildDto
{
    [Required(ErrorMessage = "O campo criador é obrigatório.")]
    public Guid OwnerId { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do servidor deve ter entre 3 e 50 caracteres.")]
    public string Name { get; set; }
}