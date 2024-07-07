using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Groups;

internal class GroupMustExistChecker(OnlineChatDbContext dbContext) : IGroupMustExistChecker
{
    public async Task<bool> CheckGroupMustExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var group = await dbContext.Groups.FindAsync(id, cancellationToken);
        return group is not null;
    }
}
