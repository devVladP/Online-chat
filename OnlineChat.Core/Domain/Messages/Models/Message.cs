using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Messages.Validators;

namespace OnlineChat.Core.Domain.Messages.Models;

public class Message : Entity
{
    public Guid Id { get; private set; }

    public string Content { get; set; }

    public Guid OwnerId { get; private set; }

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

    public static Message Create(CreateMessageData data)
    {
        Validate(new CreateMessageValidator(), data);

        return new Message(data.Content, data.OwnerId, data.GroupId);
    }
}
