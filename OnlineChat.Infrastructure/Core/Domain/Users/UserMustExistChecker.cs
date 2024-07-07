using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Users;

internal class UserMustExistChecker(OnlineChatDbContext dbContext) : IUserMustExistChecker
{
    public async Task<bool> CheckUserMustExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FindAsync(id, cancellationToken);
        return user is not null;
    }
}
