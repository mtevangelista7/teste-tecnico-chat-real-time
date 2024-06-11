using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MudBlazor;
using Refit;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;
using TesteTecnicoDiscord.Client.States;

namespace TesteTecnicoDiscord.Client.Pages
{
    public class RegisterBase : ComponentBaseExtends
    {
        [Inject] private IAuthEndpoints AuthEndpoints { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        // birthdate variables
        protected string Year;
        protected string Month;
        protected string Day;

        protected CreateUserDto UserDto = new()
            { BirthDate = null, Email = "", Name = "", Password = "", Username = "" };

        private bool _isShow;
        protected InputType PasswordInput = InputType.Password;
        protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthStateProvider
                    .GetAuthenticationStateAsync();

                var user = authState.User;

                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    NavigationManager.NavigateTo("/guilds");
                }
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }

        protected async Task HandleLoginClickAsync()
        {
            try
            {
                NavigationManager.NavigateTo("login");
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

        protected async Task HandleRegisterClickAsync(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate())
                {
                    return;
                }

                // TODO: Validate datetime

                // check if the birthdate is valid
                var birthDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
                UserDto.BirthDate = birthDate;

                await RegisterUserAsync(UserDto);

                NavigationManager.NavigateTo("/Guilds");
                Snackbar.Add("deu bom!", Severity.Success);
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }

        private async Task RegisterUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                var token = await AuthEndpoints.Register(createUserDto);

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

        protected async Task ResetAsync()
        {
            try
            {
                UserDto = new() { BirthDate = null, Email = "", Name = "", Password = "", Username = "" };
                Year = "";
                Month = "";
                Day = "";
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }
    }
}