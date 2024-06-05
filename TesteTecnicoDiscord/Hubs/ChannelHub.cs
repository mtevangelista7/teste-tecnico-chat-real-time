using Microsoft.AspNetCore.SignalR;

namespace TesteTecnicoDiscord.Hubs;

public class ChannelHub : Hub
{
    public async Task SendMessageToChannel(Guid serverId, Guid channelId, string user, string message)
    {
        string groupName = $"{serverId.ToString()}:{channelId.ToString()}";
        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// here we join the user to a channel group
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="channelId"></param>
    public async Task JoinChannel(Guid serverId, Guid channelId)
    {
        string groupName = $"{serverId.ToString()}:{channelId.ToString()}";
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary>
    /// here we remove the user from a channel group
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="channelId"></param>
    /// TODO: Aqui eu não sei bem se isso vai funcionar, visto que o usuário pode estar em vários canais ao mesmo tempo
    public async Task LeaveChannel(Guid serverId, Guid channelId)
    {
        string groupName = $"{serverId.ToString()}:{channelId.ToString()}";
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}