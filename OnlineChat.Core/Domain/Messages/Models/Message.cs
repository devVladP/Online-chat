using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Messages.Validators;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Messages.Models;

public class Message : Entity
{
    public Guid Id { get; private set; }

    public string Content { get; set; } = string.Empty;

    public Guid OwnerId { get; private set; }

    public User Owner { get; private set; }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    private Message() { }

    private Message(string content, Guid ownerId, Guid groupId)
    {
        Id = Guid.NewGuid();
        Content = content;
        OwnerId = ownerId;
        GroupId = groupId;
    }

    public async static Task<Message> Create(CreateMessageData data, 
        IUserMustExistChecker userMustExistChecker, 
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new CreateMessageValidator(userMustExistChecker), data, cancellationToken);

        return new Message(data.Content, data.OwnerId, data.GroupId);
    }
}
