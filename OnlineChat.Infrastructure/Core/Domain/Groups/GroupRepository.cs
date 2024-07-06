using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Exceptions;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Groups;

public class GroupRepository(OnlineChatDbContext dbContext) : IGroupRepository
{
    public void Add(Group group)
    {
        dbContext.Groups.Add(group);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var group = await dbContext.Groups.FindAsync(id, cancellationToken);
        dbContext.Groups.Remove(group);
    }

    public async Task<Group> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var group = await dbContext.Groups.FindAsync(id, cancellationToken);
        return group ?? throw new NotFoundException($"Group with ID {id} has not been found");
    }

    public async Task<IReadOnlyCollection<Group>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        var groups = await dbContext.Groups.Where(g => ids.Contains(g.Id)).ToListAsync(cancellationToken);
        return groups;
    }
}
