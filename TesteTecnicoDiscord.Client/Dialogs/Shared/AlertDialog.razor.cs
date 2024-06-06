using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace TesteTecnicoDiscord.Client.Dialogs.Shared
{
    public class AlertDialogBase : ComponentBase
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public string Message { get; set; } = string.Empty;

        protected void Close() => MudDialog.Close();
    }
}
