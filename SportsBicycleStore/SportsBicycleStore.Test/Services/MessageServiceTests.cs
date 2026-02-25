using FluentAssertions;
using Moq;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Exceptions;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Infastructure.Services;
using Xunit;

namespace SportsBicycleStore.Test.Services;

public class MessageServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMessageRepository> _messageRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly MessageService _service;

    public MessageServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _messageRepoMock = new Mock<IMessageRepository>();
        _userRepoMock = new Mock<IUserRepository>();

        _unitOfWorkMock.Setup(u => u.MessageRepository).Returns(_messageRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepoMock.Object);

        _service = new MessageService(_unitOfWorkMock.Object);
    }

    #region SendMessageAsync

    [Fact]
    public async Task SendMessage_Success_ReturnsMessageResponseDto()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "sender-001",
            ReceiverId = "receiver-001",
            Content = "Xin chào, tôi muốn hỏi về xe đạp này"
        };

        var sender = new Muser
        {
            UserId = "sender-001",
            UserName = "buyer1",
            FullName = "Nguyen Van A",
            Email = "buyer1@test.com",
            PasswordHash = "hash",
            RoleId = "role-buyer"
        };

        var receiver = new Muser
        {
            UserId = "receiver-001",
            UserName = "seller1",
            FullName = "Tran Van B",
            Email = "seller1@test.com",
            PasswordHash = "hash",
            RoleId = "role-seller"
        };

        var savedMessage = new Mmessage
        {
            MessageId = "msg-001",
            SenderId = dto.SenderId,
            ReceiverId = dto.ReceiverId,
            Content = dto.Content,
            IsRead = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Sender = sender,
            Receiver = receiver
        };

        _userRepoMock.Setup(r => r.GetByUserIdAsync("sender-001")).ReturnsAsync(sender);
        _userRepoMock.Setup(r => r.GetByUserIdAsync("receiver-001")).ReturnsAsync(receiver);
        _messageRepoMock.Setup(r => r.SendMessageAsync(dto)).ReturnsAsync(savedMessage);

        // Act
        var result = await _service.SendMessageAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.MessageId.Should().Be("msg-001");
        result.SenderId.Should().Be("sender-001");
        result.SenderName.Should().Be("Nguyen Van A");
        result.ReceiverId.Should().Be("receiver-001");
        result.ReceiverName.Should().Be("Tran Van B");
        result.Content.Should().Be("Xin chào, tôi muốn hỏi về xe đạp này");
        result.IsRead.Should().BeFalse();
    }

    [Fact]
    public async Task SendMessage_EmptyContent_ThrowsUserFriendlyException()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "sender-001",
            ReceiverId = "receiver-001",
            Content = ""
        };

        // Act
        Func<Task> act = async () => await _service.SendMessageAsync(dto);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(400);
        ex.Which.ErrorCode.Should().Be("INVALID_CONTENT");
    }

    [Fact]
    public async Task SendMessage_WhitespaceContent_ThrowsUserFriendlyException()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "sender-001",
            ReceiverId = "receiver-001",
            Content = "   "
        };

        // Act
        Func<Task> act = async () => await _service.SendMessageAsync(dto);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task SendMessage_SameUser_ThrowsUserFriendlyException()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "user-001",
            ReceiverId = "user-001",
            Content = "Hello"
        };

        // Act
        Func<Task> act = async () => await _service.SendMessageAsync(dto);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(400);
        ex.Which.ErrorCode.Should().Be("INVALID_RECEIVER");
    }

    [Fact]
    public async Task SendMessage_SenderNotFound_ThrowsUserFriendlyException()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "nonexistent-sender",
            ReceiverId = "receiver-001",
            Content = "Hello"
        };

        _userRepoMock.Setup(r => r.GetByUserIdAsync("nonexistent-sender")).ReturnsAsync((Muser?)null);

        // Act
        Func<Task> act = async () => await _service.SendMessageAsync(dto);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(404);
        ex.Which.ErrorCode.Should().Be("SENDER_NOT_FOUND");
    }

    [Fact]
    public async Task SendMessage_ReceiverNotFound_ThrowsUserFriendlyException()
    {
        // Arrange
        var dto = new SendMessageDto
        {
            SenderId = "sender-001",
            ReceiverId = "nonexistent-receiver",
            Content = "Hello"
        };

        var sender = new Muser
        {
            UserId = "sender-001",
            UserName = "buyer1",
            Email = "buyer1@test.com",
            PasswordHash = "hash",
            RoleId = "role-buyer"
        };

        _userRepoMock.Setup(r => r.GetByUserIdAsync("sender-001")).ReturnsAsync(sender);
        _userRepoMock.Setup(r => r.GetByUserIdAsync("nonexistent-receiver")).ReturnsAsync((Muser?)null);

        // Act
        Func<Task> act = async () => await _service.SendMessageAsync(dto);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(404);
        ex.Which.ErrorCode.Should().Be("RECEIVER_NOT_FOUND");
    }

    #endregion

    #region GetConversationAsync

    [Fact]
    public async Task GetConversation_Success_ReturnsMessageList()
    {
        // Arrange
        var sender = new Muser { UserId = "user-001", UserName = "buyer1", FullName = "Buyer", Email = "b@test.com", PasswordHash = "h", RoleId = "r" };
        var receiver = new Muser { UserId = "user-002", UserName = "seller1", FullName = "Seller", Email = "s@test.com", PasswordHash = "h", RoleId = "r" };

        var messages = new List<Mmessage>
        {
            new Mmessage
            {
                MessageId = "msg-001",
                SenderId = "user-001",
                ReceiverId = "user-002",
                Content = "Xin chào",
                IsRead = true,
                CreatedAt = DateTime.Now.AddMinutes(-10),
                Sender = sender,
                Receiver = receiver
            },
            new Mmessage
            {
                MessageId = "msg-002",
                SenderId = "user-002",
                ReceiverId = "user-001",
                Content = "Chào bạn, có gì tôi giúp được?",
                IsRead = false,
                CreatedAt = DateTime.Now.AddMinutes(-5),
                Sender = receiver,
                Receiver = sender
            }
        };

        _messageRepoMock
            .Setup(r => r.GetConversationAsync("user-001", "user-002", 1, 50))
            .ReturnsAsync(messages);

        // Act
        var result = await _service.GetConversationAsync("user-001", "user-002", 1, 50);

        // Assert
        result.Should().HaveCount(2);
        result[0].Content.Should().Be("Xin chào");
        result[1].Content.Should().Be("Chào bạn, có gì tôi giúp được?");
    }

    [Theory]
    [InlineData("", "user-002")]
    [InlineData("user-001", "")]
    [InlineData("", "")]
    [InlineData(null, "user-002")]
    [InlineData("user-001", null)]
    public async Task GetConversation_EmptyUserId_ThrowsUserFriendlyException(string? userId1, string? userId2)
    {
        // Act
        Func<Task> act = async () => await _service.GetConversationAsync(userId1!, userId2!, 1, 50);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(400);
        ex.Which.ErrorCode.Should().Be("INVALID_USER_ID");
    }

    #endregion

    #region GetConversationListAsync

    [Fact]
    public async Task GetConversationList_Success_ReturnsConversationList()
    {
        // Arrange
        var conversations = new List<ConversationDto>
        {
            new ConversationDto
            {
                UserId = "user-002",
                UserName = "seller1",
                FullName = "Tran Van B",
                LastMessage = "Xe đạp còn hàng không?",
                LastMessageAt = DateTime.Now,
                UnreadCount = 2
            }
        };

        _messageRepoMock
            .Setup(r => r.GetConversationListAsync("user-001"))
            .ReturnsAsync(conversations);

        // Act
        var result = await _service.GetConversationListAsync("user-001");

        // Assert
        result.Should().HaveCount(1);
        result[0].UserName.Should().Be("seller1");
        result[0].UnreadCount.Should().Be(2);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public async Task GetConversationList_EmptyUserId_ThrowsUserFriendlyException(string? userId)
    {
        // Act
        Func<Task> act = async () => await _service.GetConversationListAsync(userId!);

        // Assert
        var ex = await act.Should().ThrowAsync<UserFriendlyException>();
        ex.Which.StatusCode.Should().Be(400);
    }

    #endregion

    #region MarkAsReadAsync

    [Fact]
    public async Task MarkAsRead_DelegatesToRepository_ReturnsTrue()
    {
        // Arrange
        _messageRepoMock.Setup(r => r.MarkAsReadAsync("msg-001", "user-001")).ReturnsAsync(true);

        // Act
        var result = await _service.MarkAsReadAsync("msg-001", "user-001");

        // Assert
        result.Should().BeTrue();
        _messageRepoMock.Verify(r => r.MarkAsReadAsync("msg-001", "user-001"), Times.Once);
    }

    [Fact]
    public async Task MarkAsRead_MessageNotFound_ReturnsFalse()
    {
        // Arrange
        _messageRepoMock.Setup(r => r.MarkAsReadAsync("nonexistent", "user-001")).ReturnsAsync(false);

        // Act
        var result = await _service.MarkAsReadAsync("nonexistent", "user-001");

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region MarkConversationAsReadAsync

    [Fact]
    public async Task MarkConversationAsRead_DelegatesToRepository_ReturnsTrue()
    {
        // Arrange
        _messageRepoMock.Setup(r => r.MarkConversationAsReadAsync("user-001", "user-002")).ReturnsAsync(true);

        // Act
        var result = await _service.MarkConversationAsReadAsync("user-001", "user-002");

        // Assert
        result.Should().BeTrue();
        _messageRepoMock.Verify(r => r.MarkConversationAsReadAsync("user-001", "user-002"), Times.Once);
    }

    #endregion

    #region GetUnreadCountAsync

    [Fact]
    public async Task GetUnreadCount_DelegatesToRepository_ReturnsCount()
    {
        // Arrange
        _messageRepoMock.Setup(r => r.GetUnreadCountAsync("user-001")).ReturnsAsync(5);

        // Act
        var result = await _service.GetUnreadCountAsync("user-001");

        // Assert
        result.Should().Be(5);
        _messageRepoMock.Verify(r => r.GetUnreadCountAsync("user-001"), Times.Once);
    }

    #endregion
}
