namespace OnlineChat.Domain.Groups.Requests;

public record UpdateGroupRequest(string Title, Guid OwnerId);
