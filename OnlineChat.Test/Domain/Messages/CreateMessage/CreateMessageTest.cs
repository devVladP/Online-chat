using FluentAssertions;
using Moq;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Messages.Models;
using OnlineChat.Core.Domain.Users.Common;

namespace OnlineChat.Test.Domain.Messages.CreateMessage;

public class CreateMessageTest
{
    private IUserMustExistChecker UserMustExistChecker { get; }

    public CreateMessageTest()
    {
        UserMustExistChecker = Mock.Of<IUserMustExistChecker>();
    }

    [Fact]
    public async Task Should_create_message()
    {
        //Arrange
        Mock.Get(UserMustExistChecker)
            .Setup(x => x.CheckUserMustExistAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var content = "Hello, world!";
        var ownerId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var messageData = new CreateMessageData(content, ownerId, groupId);

        //Act
        var message = await Message.Create(messageData, UserMustExistChecker);

        //Assert
        message.Should().NotBeNull();
        message.Content.Should().Be(content);
    }
}
