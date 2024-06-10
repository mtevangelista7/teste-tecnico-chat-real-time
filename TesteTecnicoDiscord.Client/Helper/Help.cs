using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Client.Dialogs.Shared;

namespace TesteTecnicoDiscord.Client.Helper
{
    public class Help
    {
        public static async Task ShowAlertDialog(IDialogService DialogService, string message)
        {
            var dialog = await DialogService.ShowAsync<AlertDialog>("Alert", new DialogParameters { { "Message", message } });
            var result = await dialog.Result;
        }

        public static async Task<bool> ShowConfirmDialog(IDialogService DialogService, string message)
        {
            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirm", new DialogParameters { { "Message", message } });
            var result = await dialog.Result;

            return result.Canceled;
        }

        public static async Task HandleError(IDialogService DialogService, Exception exception, object component)
        {
            var dialog = await DialogService.ShowAsync<AlertDialog>("Error", new DialogParameters { { "Message", exception.Message } });
            var result = await dialog.Result;

            if (component is ComponentBase componentBase)
            {
            }

            // TODO: Log error
        }
    }
}
