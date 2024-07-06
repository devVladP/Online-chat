using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Users.Common;

public interface IUserRepository
{
    public Task<User> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<User>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(User user);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<User>> FindUsersByGroupId(Guid groupId, CancellationToken cancellationToken);
}
