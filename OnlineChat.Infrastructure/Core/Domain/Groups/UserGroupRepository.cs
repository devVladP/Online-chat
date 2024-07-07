using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Exceptions;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Groups;

public class UserGroupRepository(OnlineChatDbContext dbContext) : IUserGroupRepository
{
    public void Add(UserGroup userGroup)
    {
        dbContext.Add(userGroup);
    }

    public async Task DeleteAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var userGroup = await dbContext.UserGroup.FindAsync(userId, groupId, cancellationToken);
        dbContext.UserGroup.Remove(userGroup);
    }

    public async Task<UserGroup> FindAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var userGroup = await dbContext.UserGroup
            .SingleOrDefaultAsync(x => x.GroupId == groupId && x.UserId == userId, cancellationToken);

        return userGroup ?? throw new NotFoundException($"There is no {nameof(UserGroup)} relation");
    }
}
