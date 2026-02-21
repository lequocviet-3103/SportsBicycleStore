using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Gửi tin nhắn từ người bán/mua
    /// </summary>
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    {
        var result = await _messageService.SendMessageAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// Lấy lịch sử tin nhắn giữa 2 người dùng (phân trang)
    /// </summary>
    [HttpGet("conversation/{userId1}/{userId2}")]
    public async Task<IActionResult> GetConversation(
        string userId1,
        string userId2,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        var result = await _messageService.GetConversationAsync(userId1, userId2, pageNumber, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách các cuộc hội thoại của user (inbox)
    /// </summary>
    [HttpGet("conversations/{userId}")]
    public async Task<IActionResult> GetConversationList(string userId)
    {
        var result = await _messageService.GetConversationListAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// Đánh dấu 1 tin nhắn đã đọc
    /// </summary>
    [HttpPut("read/{messageId}/{userId}")]
    public async Task<IActionResult> MarkAsRead(string messageId, string userId)
    {
        var result = await _messageService.MarkAsReadAsync(messageId, userId);
        return Ok(new { success = result });
    }

    /// <summary>
    /// Đánh dấu tất cả tin nhắn trong cuộc hội thoại đã đọc
    /// </summary>
    [HttpPut("read-conversation/{senderId}/{receiverId}")]
    public async Task<IActionResult> MarkConversationAsRead(string senderId, string receiverId)
    {
        var result = await _messageService.MarkConversationAsReadAsync(senderId, receiverId);
        return Ok(new { success = result });
    }

    /// <summary>
    /// Lấy số tin nhắn chưa đọc
    /// </summary>
    [HttpGet("unread-count/{userId}")]
    public async Task<IActionResult> GetUnreadCount(string userId)
    {
        var count = await _messageService.GetUnreadCountAsync(userId);
        return Ok(new { unreadCount = count });
    }
}
