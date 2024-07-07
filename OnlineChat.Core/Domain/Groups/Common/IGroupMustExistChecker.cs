namespace OnlineChat.Core.Domain.Groups.Common;

public interface IGroupMustExistChecker
{
    Task<bool> CheckGroupMustExistAsync(Guid id, CancellationToken cancellationToken = default);
}
