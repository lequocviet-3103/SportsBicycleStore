namespace SportsBicycleStore.Application.DTO;

public class MessageResponseDto
{
    public string MessageId { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string? SenderAvatarUrl { get; set; }
    public string ReceiverId { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;
    public string? ReceiverAvatarUrl { get; set; }
    public string Content { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime? CreatedAt { get; set; }
}
