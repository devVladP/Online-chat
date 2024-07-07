using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;
using OnlineChat.Core.Exceptions;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Users;

internal class UserRepository(OnlineChatDbContext dbContext) : IUserRepository
{
    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(id, cancellationToken);
        dbContext.Users.Remove(user);
    }

    public async Task<User> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(id)
            ?? throw new NotFoundException($"There is no user with id: {id}");
        return user;
    }

    public async Task<IReadOnlyCollection<User>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Where(u => ids.Contains(u.Id)).ToListAsync(cancellationToken);
        return users;
    }

    public async Task<IReadOnlyCollection<User>> FindUsersByGroupId(Guid groupId, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users
            .Include(u => u.UserGroups)
            .Where(u => u.UserGroups.Select(ug => ug.GroupId).Contains(groupId))
            .ToListAsync(cancellationToken);
        return users;
    }
}
