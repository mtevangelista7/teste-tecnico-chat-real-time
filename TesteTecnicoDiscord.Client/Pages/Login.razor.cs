using Microsoft.AspNetCore.Components;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        protected CreateUserDto userDto = new CreateUserDto();
    }
}
