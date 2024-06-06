using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoDiscord.Application.Dtos
{
    public record LoginUserDto(string Username, string Password)
    {
        [Required(ErrorMessage = "O campo username é obrigatório.")]
        public string Username { get; set; } = Username;
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Password { get; set; } = Password;
    }
}
