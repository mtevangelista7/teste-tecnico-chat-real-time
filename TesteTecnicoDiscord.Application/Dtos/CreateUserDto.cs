using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoDiscord.Application.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(250, ErrorMessage = "O e-mail pode ter no máximo 250 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo nome de usuário é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 100 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 50 caracteres.")]
        public string Password { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}