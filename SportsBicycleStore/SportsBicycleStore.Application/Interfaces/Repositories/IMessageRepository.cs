using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;

namespace SportsBicycleStore.Application.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<Mmessage> SendMessageAsync(SendMessageDto dto);
    Task<List<Mmessage>> GetConversationAsync(string userId1, string userId2, int pageNumber, int pageSize);
    Task<List<ConversationDto>> GetConversationListAsync(string userId);
    Task<bool> MarkAsReadAsync(string messageId, string userId);
    Task<bool> MarkConversationAsReadAsync(string senderId, string receiverId);
    Task<int> GetUnreadCountAsync(string userId);
}
