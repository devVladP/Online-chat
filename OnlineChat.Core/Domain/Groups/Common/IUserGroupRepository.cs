using OnlineChat.Core.Domain.Groups.Models;

namespace OnlineChat.Core.Domain.Groups.Common;

public interface IUserGroupRepository
{
    public void Add(UserGroup userGroup);

    public Task DeleteAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);

    public Task<UserGroup> FindAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
}
