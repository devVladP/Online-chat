namespace OnlineChat.Core.Domain.Messages.Data;

public record CreateMessageData(string Content, Guid OwnerId, Guid GroupId);
