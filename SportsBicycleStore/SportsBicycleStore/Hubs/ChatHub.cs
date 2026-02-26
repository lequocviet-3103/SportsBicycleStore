using Microsoft.AspNetCore.SignalR;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;

    // Track connected users: userId -> connectionId
    private static readonly Dictionary<string, string> _connectedUsers = new();

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Called when a user connects - registers their userId with their connectionId
    /// </summary>
    public async Task RegisterUser(string userId)
    {
        _connectedUsers[userId] = Context.ConnectionId;
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        await Clients.Caller.SendAsync("Registered", userId);
    }

    /// <summary>
    /// Send a message from sender to receiver
    /// </summary>
    public async Task SendMessage(SendMessageDto dto)
    {
        var message = await _messageService.SendMessageAsync(dto);

        // Send to receiver if online
        await Clients.Group(dto.ReceiverId).SendAsync("ReceiveMessage", message);

        // Send confirmation back to sender
        await Clients.Caller.SendAsync("MessageSent", message);
    }

    /// <summary>
    /// Mark messages as read in a conversation
    /// </summary>
    public async Task MarkConversationAsRead(string senderId, string receiverId)
    {
        await _messageService.MarkConversationAsReadAsync(senderId, receiverId);

        // Notify the sender that their messages were read
        await Clients.Group(senderId).SendAsync("MessagesRead", receiverId);
    }

    /// <summary>
    /// Notify typing status
    /// </summary>
    public async Task Typing(string senderId, string receiverId)
    {
        await Clients.Group(receiverId).SendAsync("UserTyping", senderId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = _connectedUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
        if (userId != null)
        {
            _connectedUsers.Remove(userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
