namespace SportsBicycleStore.Application.DTO;

public class ConversationDto
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? LastMessage { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public int UnreadCount { get; set; }
}
