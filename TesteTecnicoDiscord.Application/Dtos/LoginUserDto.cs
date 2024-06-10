using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoDiscord.Application.Dtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "O campo username é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário tem pelo menos 3 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário tem pelo menos 8 caracteres")]
        public string Password { get; set; }
    }
}