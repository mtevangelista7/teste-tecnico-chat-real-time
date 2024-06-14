using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Dialogs;

public class UserProfileDialogBase : ComponentBaseExtends
{
    [Inject] private IUserEndpoints UserEndpoints { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    protected GetUserDto User = new();
    protected int MessagesCount;
    protected int GuildsCount;

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