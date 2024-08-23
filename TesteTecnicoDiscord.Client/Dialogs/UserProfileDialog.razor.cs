using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Dialogs;

public class UserProfileDialogBase : ComponentBaseExtends
{
    protected int GuildsCount;
    protected int MessagesCount;

    protected GetUserDto User = new();
    [Inject] private IUserEndpoints UserEndpoints { get; set; }
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            User = await UserEndpoints.GetUser();

            await GetMessageCount();
            await GetGuildCount();

            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    private async Task GetMessageCount()
    {
        MessagesCount = await UserEndpoints.GetMessagesCount(User.Id);
    }

    private async Task GetGuildCount()
    {
        GuildsCount = await UserEndpoints.GetGuildCount(User.Id);
    }

    protected async Task CloseDialog()
    {
        try
        {
            MudDialog.Close();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }
}