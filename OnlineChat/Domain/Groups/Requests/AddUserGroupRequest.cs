namespace OnlineChat.Domain.Groups.Requests;

public record AddUserGroupRequest(Guid UserId, Guid GroupId);
