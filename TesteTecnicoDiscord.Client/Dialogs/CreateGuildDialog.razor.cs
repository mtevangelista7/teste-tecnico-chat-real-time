using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Dialogs;

public class CreateGuildDialogBase : ComponentBaseExtends
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public Guid OwnerId { get; set; } = Guid.Empty;
    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    protected CreateGuildDto CreateGuildDto = new();

    protected void Cancel() => MudDialog.Cancel();

    protected override void OnInitialized()
    {
        if (OwnerId.Equals(Guid.Empty))
            Cancel();

        CreateGuildDto.OwnerId = OwnerId;
    }

    protected async Task HandleCreateClickAsync(EditContext context)
    {
        try
        {
            if (!context.Validate())
                return;

            // create a guild
            await GuildsEndpoints.CrateNewGuild(CreateGuildDto);

            MudDialog.Close();
            Snackbar.Add("Servidor criado com sucesso!", Severity.Success);
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }
}