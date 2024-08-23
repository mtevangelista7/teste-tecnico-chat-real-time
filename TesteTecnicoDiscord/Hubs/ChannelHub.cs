using Microsoft.AspNetCore.SignalR;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;

namespace TesteTecnicoDiscord.Hubs;

public class ChannelHub(IUserService userService, IMessageService messageService) : Hub
{
    private static readonly Dictionary<string, HashSet<string>> ConnectionGroups = new();

    public async Task SendMessage(CreateMessageDto messageDto)
    {
        if (messageDto is null)
            throw new NullReferenceException();

        var result = await messageService.Add(messageDto);

        if (!result.IsSuccess)
        {
            var errorMessage = new ErrorMessageDto
            {
                ErrorCode = "MESSAGE_CREATION_FAILED",
                ErrorMessage = "Failed to create the message. Please try again later."
            };

            await Clients.Caller.SendAsync("ReceiveError", errorMessage);
            return;
        }
        
        var user = await userService.GetById(result.Value.UserId);

        if (user is null)
        {
            var errorMessage = new ErrorMessageDto
            {
                ErrorCode = "USER_NOT_FOUND",
                ErrorMessage = "User not found."
            };

            await Clients.Caller.SendAsync("ReceiveError", errorMessage);
            return;
        }

        var receiveMessage = new ReceiveMessageDto
        {
            Id = result.Value.Id,
            Content = result.Value.Content,
            OwnerUsername = user.Username,
            Timestamp = result.Value.Timestamp,
            UserId = user.Id
        };

        await Clients.Group(messageDto.ChannelId.ToString()).SendAsync("ReceiveMessage", receiveMessage);
    }

    public async Task JoinChannel(Guid guildId, Guid channelId, Guid userId)
    {
        var user = await userService.GetById(userId);
        var messageDto = new CreateMessageDto
        {
            ChannelId = channelId,
            Content = $"O usuário {user.Username} entrou do chat",
            GuildId = guildId,
            Timestamp = DateTime.Now,
            UserId = user.Id
        };

        if (!ConnectionGroups.ContainsKey(Context.ConnectionId)) ConnectionGroups[Context.ConnectionId] = [];

        ConnectionGroups[Context.ConnectionId].Add(channelId.ToString());

        await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
        await SendMessage(messageDto);
    }

    public async Task LeaveChannel(Guid guildId, Guid channelId, Guid userId)
    {
        var groupName = channelId.ToString();
        if (ConnectionGroups.ContainsKey(Context.ConnectionId) &&
            ConnectionGroups[Context.ConnectionId].Contains(groupName))
        {
            var user = await userService.GetById(userId);
            var messageDto = new CreateMessageDto
            {
                ChannelId = channelId,
                Content = $"O usuário {user.Username} saiu do chat",
                GuildId = guildId,
                Timestamp = DateTime.Now,
                UserId = user.Id
            };

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            ConnectionGroups[Context.ConnectionId].Remove(groupName);

            if (ConnectionGroups[Context.ConnectionId].Count == 0) ConnectionGroups.Remove(Context.ConnectionId);

            await SendMessage(messageDto);
        }
    }
}