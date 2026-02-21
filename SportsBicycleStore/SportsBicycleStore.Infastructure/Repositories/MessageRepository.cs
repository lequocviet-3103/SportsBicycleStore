using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Infastructure.Data;

namespace SportsBicycleStore.Infastructure.Repositories;

public class MessageRepository : Repository<Mmessage>, IMessageRepository
{
    public MessageRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Mmessage> SendMessageAsync(SendMessageDto dto)
    {
        var message = new Mmessage
        {
            MessageId = Guid.NewGuid().ToString(),
            SenderId = dto.SenderId,
            ReceiverId = dto.ReceiverId,
            Content = dto.Content,
            IsRead = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _context.Mmessages.AddAsync(message);
        await _context.SaveChangesAsync();

        // Load navigation properties
        await _context.Entry(message).Reference(m => m.Sender).LoadAsync();
        await _context.Entry(message).Reference(m => m.Receiver).LoadAsync();

        return message;
    }

    public async Task<List<Mmessage>> GetConversationAsync(string userId1, string userId2, int pageNumber, int pageSize)
    {
        return await _context.Mmessages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m =>
                (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                (m.SenderId == userId2 && m.ReceiverId == userId1))
            .OrderByDescending(m => m.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ConversationDto>> GetConversationListAsync(string userId)
    {
        // Get all messages where user is sender or receiver
        var conversations = await _context.Mmessages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
            .Select(g => new
            {
                OtherUserId = g.Key,
                LastMessage = g.OrderByDescending(m => m.CreatedAt).First(),
                UnreadCount = g.Count(m => m.ReceiverId == userId && m.IsRead == false)
            })
            .ToListAsync();

        var result = new List<ConversationDto>();
        foreach (var conv in conversations)
        {
            var otherUser = await _context.Musers.FindAsync(conv.OtherUserId);
            if (otherUser != null)
            {
                result.Add(new ConversationDto
                {
                    UserId = otherUser.UserId,
                    UserName = otherUser.UserName,
                    FullName = otherUser.FullName,
                    AvatarUrl = otherUser.AvatarUrl,
                    LastMessage = conv.LastMessage.Content,
                    LastMessageAt = conv.LastMessage.CreatedAt,
                    UnreadCount = conv.UnreadCount
                });
            }
        }

        return result.OrderByDescending(c => c.LastMessageAt).ToList();
    }

    public async Task<bool> MarkAsReadAsync(string messageId, string userId)
    {
        var message = await _context.Mmessages
            .FirstOrDefaultAsync(m => m.MessageId == messageId && m.ReceiverId == userId);

        if (message == null) return false;

        message.IsRead = true;
        message.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkConversationAsReadAsync(string senderId, string receiverId)
    {
        var unreadMessages = await _context.Mmessages
            .Where(m => m.SenderId == senderId && m.ReceiverId == receiverId && m.IsRead == false)
            .ToListAsync();

        if (!unreadMessages.Any()) return false;

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
            message.UpdatedAt = DateTime.Now;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _context.Mmessages
            .CountAsync(m => m.ReceiverId == userId && m.IsRead == false);
    }
}
