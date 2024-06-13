using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Client.CustomComponentBase;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Client.RefitInterfaces;

namespace TesteTecnicoDiscord.Client.Pages;

public class ChannelMessagesBase : ComponentBaseExtends, IDisposable
{
    [Parameter] public Guid GuildId { get; set; }
    [Parameter] public Guid ChannelId { get; set; }

    [Inject] private IGuildsEndpoints GuildsEndpoints { get; set; }

    protected string MessageInput = string.Empty;
    protected List<ReceiveMessageDto> ListMessages = [];
    protected string ChannelName = string.Empty;

    private HubConnection _hubConnection;
    private DotNetObjectReference<ChannelMessagesBase> _dotNetRef;
    private Guid _userId = Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var authState = await AuthStateProvider
                .GetAuthenticationStateAsync();

            _userId = Guid.Parse(authState.User.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value);
            if (_userId.Equals(Guid.Empty))
            {
                NavigationManager.NavigateTo("/login");
                return;
            }

            _dotNetRef = DotNetObjectReference.Create(this);
            NavigationManager.LocationChanged += HandleLocationChanged;
            await JSRuntime.InvokeVoidAsync("addBeforeUnloadListener", _dotNetRef);

            await OpenConnection();
            await LoadMessages();
            await GetChannelName();

            StateHasChanged();
            await JSRuntime.InvokeVoidAsync("scrollToBottom", "scrollablePaper");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
        }
    }

    private async Task OpenConnection()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/channelHub"))
            .Build();

        _hubConnection.On<ReceiveMessageDto>("ReceiveMessage", async (message) =>
        {
            ListMessages.Add(message);
            StateHasChanged();

            await JSRuntime.InvokeVoidAsync("scrollToBottom", "scrollablePaper");
        });

        await _hubConnection.StartAsync();
        await _hubConnection.SendAsync("JoinChannel", GuildId, ChannelId, _userId);
    }

    private async Task LoadMessages()
    {
        ListMessages = await GuildsEndpoints.GetAllMessagesFromChannel(ChannelId);
        StateHasChanged();
    }

    protected async Task SendMessage()
    {
        try
        {
            // send the message to the hub
            if (!string.IsNullOrEmpty(MessageInput))
            {
                var createMessageDto = new CreateMessageDto()
                {
                    ChannelId = ChannelId,
                    Content = MessageInput,
                    GuildId = GuildId,
                    Timestamp = DateTime.Now,
                    UserId = _userId
                };

                await _hubConnection.SendAsync("SendMessage", createMessageDto);
                MessageInput = string.Empty;
            }
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
    {
        Task.Run(async () => await OnPageExit());
    }

    [JSInvokable]
    public async Task HandleBeforeUnload()
    {
        await OnPageExit();
    }

    private async Task OnPageExit()
    {
        await _hubConnection.SendAsync("LeaveChannel", GuildId, ChannelId, _userId);
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;
        DisposeAsync();
    }

    private async void DisposeAsync()
    {
        await JSRuntime.InvokeVoidAsync("removeBeforeUnloadListener", _dotNetRef);
        _dotNetRef?.Dispose();
        await OnPageExit();
    }

    protected async Task OnClickLeave()
    {
        try
        {
            NavigationManager.NavigateTo($"/Guilds/{GuildId}");
        }
        catch (Exception ex)
        {
            await Help.HandleError(DialogService, ex, this);
        }
    }

    private async Task GetChannelName()
    {
        var channel = await GuildsEndpoints.GetChannelById(ChannelId);
        ChannelName = channel.Name;
    }
}