using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Exceptions;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;

namespace SportsBicycleStore.Infastructure.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponseDto> SendMessageAsync(SendMessageDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
            throw new UserFriendlyException(400, "INVALID_CONTENT", "Nội dung tin nhắn không được để trống.");

        if (dto.SenderId == dto.ReceiverId)
            throw new UserFriendlyException(400, "INVALID_RECEIVER", "Không thể gửi tin nhắn cho chính mình.");

        // Verify sender exists
        var sender = await _unitOfWork.UserRepository.GetByUserIdAsync(dto.SenderId);
        if (sender == null)
            throw new UserFriendlyException(404, "SENDER_NOT_FOUND", "Không tìm thấy người gửi.");

        // Verify receiver exists
        var receiver = await _unitOfWork.UserRepository.GetByUserIdAsync(dto.ReceiverId);
        if (receiver == null)
            throw new UserFriendlyException(404, "RECEIVER_NOT_FOUND", "Không tìm thấy người nhận.");

        var message = await _unitOfWork.MessageRepository.SendMessageAsync(dto);

        return MapToResponseDto(message);
    }

    public async Task<List<MessageResponseDto>> GetConversationAsync(string userId1, string userId2, int pageNumber = 1, int pageSize = 50)
    {
        if (string.IsNullOrWhiteSpace(userId1) || string.IsNullOrWhiteSpace(userId2))
            throw new UserFriendlyException(400, "INVALID_USER_ID", "UserId không được để trống.");

        var messages = await _unitOfWork.MessageRepository.GetConversationAsync(userId1, userId2, pageNumber, pageSize);

        return messages.Select(MapToResponseDto).ToList();
    }

    public async Task<List<ConversationDto>> GetConversationListAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new UserFriendlyException(400, "INVALID_USER_ID", "UserId không được để trống.");

        return await _unitOfWork.MessageRepository.GetConversationListAsync(userId);
    }

    public async Task<bool> MarkAsReadAsync(string messageId, string userId)
    {
        return await _unitOfWork.MessageRepository.MarkAsReadAsync(messageId, userId);
    }

    public async Task<bool> MarkConversationAsReadAsync(string senderId, string receiverId)
    {
        return await _unitOfWork.MessageRepository.MarkConversationAsReadAsync(senderId, receiverId);
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _unitOfWork.MessageRepository.GetUnreadCountAsync(userId);
    }

    private static MessageResponseDto MapToResponseDto(Mmessage message)
    {
        return new MessageResponseDto
        {
            MessageId = message.MessageId,
            SenderId = message.SenderId,
            SenderName = message.Sender?.FullName ?? message.Sender?.UserName ?? "",
            SenderAvatarUrl = message.Sender?.AvatarUrl,
            ReceiverId = message.ReceiverId,
            ReceiverName = message.Receiver?.FullName ?? message.Receiver?.UserName ?? "",
            ReceiverAvatarUrl = message.Receiver?.AvatarUrl,
            Content = message.Content,
            IsRead = message.IsRead ?? false,
            CreatedAt = message.CreatedAt
        };
    }
}
