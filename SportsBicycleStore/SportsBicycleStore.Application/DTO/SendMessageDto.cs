namespace SportsBicycleStore.Application.DTO;

public class SendMessageDto
{
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
    public string Content { get; set; } = null!;
}
