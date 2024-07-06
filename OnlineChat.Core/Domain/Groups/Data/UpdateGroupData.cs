namespace OnlineChat.Core.Domain.Groups.Data;

public record UpdateGroupData(
    string Title,
    Guid OwnerId
    );
