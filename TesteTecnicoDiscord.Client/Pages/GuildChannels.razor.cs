using System.Threading.Channels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Dialogs;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;
using TesteTecnicoDiscord.Client.States;

namespace TesteTecnicoDiscord.Client.Pages;

public class GuildChannelsBase : ComponentBaseExtends
{
    [Parameter] public Guid GuildId { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }

    protected string Username = string.Empty;
    protected bool Processing;
    private Guid _userId = Guid.Empty;

    protected List<GetChannelsDto> Channels = [];
    protected List<GetChannelsDto> FilteredChannels = [];
    protected GetGuildsDto GuildMain = new();

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

            GuildMain = await GuildsEndpoints.GetGuild(GuildId);
            Channels = await GetAllChannels(GuildId);

            FilteredChannels = Channels;
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    private async Task<List<GetChannelsDto>> GetAllChannels(Guid guildId)
    {
        return await GuildsEndpoints.GetChannels(guildId);
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

    protected async Task OnClickAddNewChannel()
    {
        try
        {
            Processing = true;

            // open the dialog
            var dialog = await DialogService.ShowAsync<CreateGuildOrChannelDialog>("Criar novo canal",
                new DialogParameters { { "GuildMainId", GuildId } });
            var result = await dialog.Result;

            Processing = false;

            if (result.Canceled)
                return;

            Channels = await GetAllChannels(GuildId);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task JoinChannelMessage(GetChannelsDto getChannelsDto)
    {
        try
        {
            // join in channel
            NavigationManager.NavigateTo($"/Guilds/{GuildId}/{getChannelsDto.Id}");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task OnClickBackToGuilds()
    {
        try
        {
            NavigationManager.NavigateTo("/Guilds");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected async Task FilterChannels(string param)
    {
        FilteredChannels = string.IsNullOrWhiteSpace(param)
            ? Channels
            : Channels.FindAll(c => c.Name.Contains(param, StringComparison.OrdinalIgnoreCase));

        StateHasChanged();
    }
}