using OnlineChat.Core.Common;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Common;

public class UnitOfWork(OnlineChatDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
