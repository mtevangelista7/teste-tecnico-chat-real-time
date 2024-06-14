using Microsoft.AspNetCore.SignalR;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;

namespace TesteTecnicoDiscord.Hubs;

public class ChannelHub(IUserService userService, IMessageService messageService) : Hub
{
    private static readonly Dictionary<string, HashSet<string>> ConnectionGroups = new();

    public async Task SendMessage(CreateMessageDto messageDto)
    {
        if (messageDto is null)
            throw new NullReferenceException();
        
        var newMessage = await messageService.Add(messageDto);

        var user = await userService.GetById(newMessage.UserId);

        if (user is null)
            throw new NullReferenceException();

        var receiveMessage = new ReceiveMessageDto()
        {
            Id = newMessage.Id,
            Content = newMessage.Content,
            OwnerUsername = user.Username,
            Timestamp = newMessage.Timestamp,
            UserId = user.Id
        };

        await Clients.Group(messageDto.ChannelId.ToString()).SendAsync("ReceiveMessage", receiveMessage);
    }

    public async Task JoinChannel(Guid guildId, Guid channelId, Guid userId)
    {
        var user = await userService.GetById(userId);
        var messageDto = new CreateMessageDto()
        {
            ChannelId = channelId,
            Content = $"O usuário {user.Username} entrou do chat",
            GuildId = guildId,
            Timestamp = DateTime.Now,
            UserId = user.Id
        };

        if (!ConnectionGroups.ContainsKey(Context.ConnectionId))
        {
            ConnectionGroups[Context.ConnectionId] = [];
        }

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
            var messageDto = new CreateMessageDto()
            {
                ChannelId = channelId,
                Content = $"O usuário {user.Username} saiu do chat",
                GuildId = guildId,
                Timestamp = DateTime.Now,
                UserId = user.Id
            };

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            ConnectionGroups[Context.ConnectionId].Remove(groupName);

            if (ConnectionGroups[Context.ConnectionId].Count == 0)
            {
                ConnectionGroups.Remove(Context.ConnectionId);
            }

            await SendMessage(messageDto);
        }
    }
}