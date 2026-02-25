using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Controllers;
using Xunit;

namespace SportsBicycleStore.Test.Controllers;

public class MessageControllerTests
{
    private readonly Mock<IMessageService> _messageServiceMock;
    private readonly MessageController _controller;

    public MessageControllerTests()
    {
        _messageServiceMock = new Mock<IMessageService>();
        _controller = new MessageController(_messageServiceMock.Object);
    }

    [Fact]
    public async Task SendMessage_ReturnsOkResult()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "sender-001",
            ReceiverId = "receiver-001",
            Content = "Xe đạp còn hàng không?"
        };

        var response = new MessageResponseDto
        {
            MessageId = "msg-001",
            SenderId = "sender-001",
            SenderName = "Buyer",
            ReceiverId = "receiver-001",
            ReceiverName = "Seller",
            Content = "Xe đạp còn hàng không?",
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        _messageServiceMock.Setup(s => s.SendMessageAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.SendMessage(dto);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var value = okResult.Value.Should().BeOfType<MessageResponseDto>().Subject;
        value.MessageId.Should().Be("msg-001");
        value.Content.Should().Be("Xe đạp còn hàng không?");
    }

    [Fact]
    public async Task GetConversation_ReturnsOkResult()
    {
        // Arrange
        var messages = new List<MessageResponseDto>
        {
            new MessageResponseDto
            {
                MessageId = "msg-001",
                SenderId = "user-001",
                SenderName = "Buyer",
                ReceiverId = "user-002",
                ReceiverName = "Seller",
                Content = "Hello",
                IsRead = true,
                CreatedAt = DateTime.Now
            }
        };

        _messageServiceMock
            .Setup(s => s.GetConversationAsync("user-001", "user-002", 1, 50))
            .ReturnsAsync(messages);

        // Act
        var result = await _controller.GetConversation("user-001", "user-002", 1, 50);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var value = okResult.Value.Should().BeAssignableTo<List<MessageResponseDto>>().Subject;
        value.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetConversationList_ReturnsOkResult()
    {
        // Arrange
        var conversations = new List<ConversationDto>
        {
            new ConversationDto
            {
                UserId = "user-002",
                UserName = "seller1",
                FullName = "Seller",
                LastMessage = "OK, để tôi check",
                LastMessageAt = DateTime.Now,
                UnreadCount = 1
            }
        };

        _messageServiceMock
            .Setup(s => s.GetConversationListAsync("user-001"))
            .ReturnsAsync(conversations);

        // Act
        var result = await _controller.GetConversationList("user-001");

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var value = okResult.Value.Should().BeAssignableTo<List<ConversationDto>>().Subject;
        value.Should().HaveCount(1);
        value[0].UnreadCount.Should().Be(1);
    }

    [Fact]
    public async Task MarkAsRead_ReturnsOkWithSuccess()
    {
        // Arrange
        _messageServiceMock.Setup(s => s.MarkAsReadAsync("msg-001", "user-001")).ReturnsAsync(true);

        // Act
        var result = await _controller.MarkAsRead("msg-001", "user-001");

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        // Anonymous object - check via dynamic or string
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task MarkConversationAsRead_ReturnsOkWithSuccess()
    {
        // Arrange
        _messageServiceMock.Setup(s => s.MarkConversationAsReadAsync("user-001", "user-002")).ReturnsAsync(true);

        // Act
        var result = await _controller.MarkConversationAsRead("user-001", "user-002");

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUnreadCount_ReturnsOkWithCount()
    {
        // Arrange
        _messageServiceMock.Setup(s => s.GetUnreadCountAsync("user-001")).ReturnsAsync(3);

        // Act
        var result = await _controller.GetUnreadCount("user-001");

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().NotBeNull();
    }
}
