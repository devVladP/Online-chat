namespace OnlineChat.Domain.Groups.Requests;

public record CreateGroupRequest(string Title, Guid OwnerId);

