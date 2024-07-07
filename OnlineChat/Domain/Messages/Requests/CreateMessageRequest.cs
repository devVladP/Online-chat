namespace OnlineChat.Domain.Messages.Requests;

public record CreateMessageRequest(string Content, Guid OwnerId, Guid GroupId);
