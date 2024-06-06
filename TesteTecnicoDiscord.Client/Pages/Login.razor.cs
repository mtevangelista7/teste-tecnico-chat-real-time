using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.Helper;

namespace TesteTecnicoDiscord.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        protected LoginUserDto userDto = new LoginUserDto("", ""); // TODO: Usar outra record
        protected bool isShow;
        protected InputType PasswordInput = InputType.Password;
        protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        protected async Task HandleRegisterClickAsync()
        {
            try
            {
                NavigationManager.NavigateTo("register");
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }

        protected void ShowPassword()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
