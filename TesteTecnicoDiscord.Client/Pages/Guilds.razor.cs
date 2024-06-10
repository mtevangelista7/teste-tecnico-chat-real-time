using Microsoft.AspNetCore.Components;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Pages;

public class GuildsBase : ComponentBaseExtends
{
    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }

    protected string Username = string.Empty;
    private Guid _userId = Guid.Empty;

    protected List<GetGuildsDto> Guilds = [];

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
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }
}