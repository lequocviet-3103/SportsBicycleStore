using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;

namespace SportsBicycleStore.Application.Interfaces.Services;

public interface IMessageService
{
    Task<MessageResponseDto> SendMessageAsync(SendMessageDto dto);
    Task<List<MessageResponseDto>> GetConversationAsync(string userId1, string userId2, int pageNumber = 1, int pageSize = 50);
    Task<List<ConversationDto>> GetConversationListAsync(string userId);
    Task<bool> MarkAsReadAsync(string messageId, string userId);
    Task<bool> MarkConversationAsReadAsync(string senderId, string receiverId);
    Task<int> GetUnreadCountAsync(string userId);
}
