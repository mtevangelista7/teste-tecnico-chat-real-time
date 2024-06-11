using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Dialogs;

public class CreateGuildOrChannelDialogBase : ComponentBaseExtends
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public Guid OwnerUserId { get; set; } = Guid.Empty;
    [Parameter] public Guid GuildMainId { get; set; } = Guid.Empty;
    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    protected CreateGuildDto CreateGuildDto = new();
    protected CreateChannelDto CreateChannelDto = new();
    protected bool CreateGuild = false;

    protected void Cancel() => MudDialog.Cancel();

    protected override void OnInitialized()
    {
        if (OwnerUserId.Equals(Guid.Empty))
        {
            CreateGuild = false;
            CreateChannelDto.GuildId = GuildMainId;
        }
        else
        {
            CreateGuild = true;
            CreateGuildDto.OwnerId = OwnerUserId;
        }
    }

    protected async Task HandleCreateGuildClickAsync(EditContext context)
    {
        try
        {
            if (!context.Validate())
                return;

            // create a guild
            await GuildsEndpoints.CreateNewGuild(CreateGuildDto);

            MudDialog.Close();
            Snackbar.Add("Servidor criado com sucesso!", Severity.Success);
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task HandleCreateChannelClickAsync(EditContext context)
    {
        try
        {
            if (!context.Validate())
                return;

            // create a channel
            await GuildsEndpoints.CreateNewChannel(GuildMainId, CreateChannelDto);

            MudDialog.Close();
            Snackbar.Add("Canal criado com sucesso!", Severity.Success);
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }
}