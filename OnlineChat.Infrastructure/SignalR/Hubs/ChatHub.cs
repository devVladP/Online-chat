using Microsoft.AspNetCore.SignalR;

namespace OnlineChat.Infrastructure.SignalR.Hubs;
public sealed class ChatHub : Hub
{
    public async Task SendMessageToGroup(Guid groupId, Guid userId, string message)
    {
        await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", userId, message);
    }

    public async Task JoinGroup(Guid groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
    }

    public async Task LeaveGroup(Guid groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
    }

    public async Task NotifyGroupDeleted(Guid groupId)
    {
        await Clients.Group(groupId.ToString()).SendAsync("GroupDeleted", groupId.ToString());
    }
}
