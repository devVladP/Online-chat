using OnlineChat.Core.Domain.Groups.Models;

namespace OnlineChat.Core.Domain.Groups.Common;

public interface IGroupRepository
{
    public Task<Group> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Group>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Group group);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
