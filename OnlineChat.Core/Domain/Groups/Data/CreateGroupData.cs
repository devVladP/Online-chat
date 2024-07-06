namespace OnlineChat.Core.Domain.Groups.Data;

public record CreateGroupData(
    string Title,
    Guid OwnerId
    );
