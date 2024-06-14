using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Dialogs;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;
using TesteTecnicoDiscord.Client.States;

namespace TesteTecnicoDiscord.Client.Pages;

public class GuildsBase : ComponentBaseExtends
{
    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    protected string Username = string.Empty;
    protected bool Processing = false;
    private Guid _userId = Guid.Empty;

    protected List<GetGuildsDto> Guilds = [];
    protected List<GetGuildsDto> FilteredGuilds = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var authState = await AuthStateProvider
                .GetAuthenticationStateAsync();

            var user = authState.User;

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/login");
            }

            Username = user.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            _userId = Guid.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value);

            if (string.IsNullOrWhiteSpace(Username) || _userId.Equals(Guid.Empty))
            {
                NavigationManager.NavigateTo("/login");
                return;
            }

            Guilds = await GetGuilds();
            FilteredGuilds = Guilds;
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    private async Task<List<GetGuildsDto>> GetGuilds()
    {
        return await GuildsEndpoints.GetGuilds();
    }

    protected async Task LogOutUser()
    {
        try
        {
            var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            await customAuthenticationStateProvider.LogOut();

            NavigationManager.NavigateTo("/login");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task OnClickAddNewGuild()
    {
        try
        {
            Processing = true;

            // open the dialog
            var dialog = await DialogService.ShowAsync<CreateGuildOrChannelDialog>("Criar novo servidor",
                new DialogParameters { { "OwnerUserId", _userId } });
            var result = await dialog.Result;

            Processing = false;

            if (result.Canceled)
                return;

            Guilds = await GetGuilds();
            FilteredGuilds = Guilds;
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task OnClickDelete(GetGuildsDto guildDto)
    {
        try
        {
            if (guildDto.Id.Equals(Guid.Empty))
                await Help.ShowAlertDialog(DialogService, "Erro ao tentar deletar servidor!");

            var confirm =
                await Help.ShowConfirmDialog(DialogService, $"Confirma a exclusão do servidor? {guildDto.Name}");

            if (!confirm)
                return;

            await GuildsEndpoints.DeleteGuild(guildDto.Id);

            Snackbar.Add("Servidor deletado com sucesso", Severity.Success);

            Guilds = await GetGuilds();
            FilteredGuilds = Guilds;
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task OnClickJoin(GetGuildsDto guildDto)
    {
        try
        {
            NavigationManager.NavigateTo($"/Guilds/{guildDto.Id}");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task FilterGuilds(string param)
    {
        FilteredGuilds = string.IsNullOrWhiteSpace(param)
            ? Guilds
            : Guilds.FindAll(c => c.Name.Contains(param, StringComparison.OrdinalIgnoreCase));

        StateHasChanged();
    }
}