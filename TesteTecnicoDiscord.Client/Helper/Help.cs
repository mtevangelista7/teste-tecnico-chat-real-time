using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Client.Dialogs.Shared;

namespace TesteTecnicoDiscord.Client.Helper
{
    public class Help
    {
        public static async Task ShowAlertDialog(IDialogService dialogService, string message)
        {
            var dialog = await dialogService.ShowAsync<AlertDialog>("Alert", new DialogParameters { { "Message", message } });
            var result = await dialog.Result;
        }

        public static async Task<bool> ShowConfirmDialog(IDialogService dialogService, string message)
        {
            var dialog = await dialogService.ShowAsync<ConfirmDialog>("Confirm", new DialogParameters { { "Message", message } });
            var result = await dialog.Result;

            return !result.Canceled;
        }

        public static async Task HandleError(IDialogService dialogService, Exception exception, object component)
        {
            var dialog = await dialogService.ShowAsync<AlertDialog>("Error", new DialogParameters { { "Message", exception.Message } });
            var result = await dialog.Result;

            if (component is ComponentBase componentBase)
            {
            }

            // TODO: Log error
        }
    }
}
