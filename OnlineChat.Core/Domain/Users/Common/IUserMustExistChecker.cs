namespace OnlineChat.Core.Domain.Users.Common;

public interface IUserMustExistChecker
{
    public Task<bool> CheckUserMustExistAsync(Guid id, CancellationToken cancellationToken = default);
}
