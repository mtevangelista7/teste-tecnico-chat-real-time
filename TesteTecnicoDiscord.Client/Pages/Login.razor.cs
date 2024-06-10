using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;
using TesteTecnicoDiscord.Client.States;

namespace TesteTecnicoDiscord.Client.Pages
{
    public class LoginBase : ComponentBaseExtends
    {
        [Inject] private IAuthEndpoints AuthEndpoints { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        protected LoginUserDto UserDto = new() { Password = "", Username = "" };
        private bool _isShow;
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
            if (_isShow)
            {
                _isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                _isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        protected async Task HandleLoginClickAsync(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate())
                {
                    return;
                }

                await LoginUserAsync(UserDto);
                
                // go to channel page (?)
                NavigationManager.NavigateTo("/Guilds");
                Snackbar.Add("deu bom!", Severity.Success);
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }

        private async Task LoginUserAsync(LoginUserDto loginUserDto)
        {
            try
            {
                var token = await AuthEndpoints.Login(loginUserDto);

                if (string.IsNullOrWhiteSpace(token))
                    throw new NullReferenceException();

                var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
                await customAuthenticationStateProvider.UpdateAuthenticationStateAsync(token);
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }
    }
}