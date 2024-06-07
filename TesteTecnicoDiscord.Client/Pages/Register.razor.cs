using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.Helper;

namespace TesteTecnicoDiscord.Client.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        // birth date variables
        protected int year;
        protected int month;
        protected int day;

        protected CreateUserDto userDto = new CreateUserDto("", "", "", "", null);
        protected bool isShow;
        protected InputType PasswordInput = InputType.Password;
        protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

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

        protected async Task HandleRegisterClickAsync(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate())
                {
                    return;
                }

                // check if the birth date is valid
                var birthDate = new DateTime(year, month, day);
                userDto.BirthDate = birthDate;

                await RegisterUserAsync(userDto);
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
                userDto = new CreateUserDto("", "", "", "", null);
                year = 0;
                month = 0;
                day = 0;
            }
            catch (Exception ex)
            {
                await Help.HandleError(DialogService, ex, this);
            }
        }
    }
}