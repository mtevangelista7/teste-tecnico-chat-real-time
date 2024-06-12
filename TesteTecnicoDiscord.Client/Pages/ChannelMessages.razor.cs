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

    private HubConnection _hubConnection;
    private DotNetObjectReference<ChannelMessagesBase> _dotNetRef;
    private Guid _userId = Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            NavigationManager.LocationChanged += HandleLocationChanged;
            await JSRuntime.InvokeVoidAsync("addBeforeUnloadListener", _dotNetRef);

            // connect to the hub and load the messages
            await OpenConnection();
            await LoadMessages();
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
            .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
            .Build();

        // receive the message from the hub
        _hubConnection.On<Guid, string, DateTime>("ReceiveMessage", (userId, text, timestamp) =>
        {
            // TODO: Adicionar na lista de mensagens da tela
            StateHasChanged();
        });

        // TODO: Colocar o user para entrar no channel
        await _hubConnection.StartAsync();
        await _hubConnection.SendAsync("JoinChat",);
    }

    private async Task LoadMessages()
    {
        // TODO: Buscar na base todas as mensagens
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
        // Chame seu método aqui
        OnPageExit();
    }

    [JSInvokable]
    public Task HandleBeforeUnload()
    {
        // Chame seu método aqui
        OnPageExit();
        return Task.CompletedTask;
    }

    private void OnPageExit()
    {
        // Coloque sua lógica aqui
        Console.WriteLine("Usuário está saindo da página");
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;
        _ = JSRuntime.InvokeVoidAsync("removeBeforeUnloadListener", _dotNetRef);
        _dotNetRef?.Dispose();
    }
}