using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Domain.Messages.Common;
using OnlineChat.Core.Domain.Messages.Models;
using OnlineChat.Core.Exceptions;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Core.Domain.Messages;

internal class MessageRepository(OnlineChatDbContext dbContext) : IMessageRepository
{
    public void Add(Message message)
    {
        dbContext.Messages.Add(message);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var message = await dbContext.Messages.FindAsync(id, cancellationToken);
        dbContext.Messages.Remove(message);
    }

    public async Task<Message> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var message = await dbContext.Messages.FindAsync(id, cancellationToken);
        return message ?? throw new NotFoundException($"Message with ID {id} has not been found");
    }

    public async Task<IReadOnlyCollection<Message>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        var messages = await dbContext.Messages.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        return messages;
    }

    public async Task<IReadOnlyCollection<Message>> FindMessagesByUserId(Guid id, CancellationToken cancellationToken)
    {
        var messages = await dbContext.Messages.Where(x => x.OwnerId == id).ToListAsync(cancellationToken);
        return messages;
    }

    public async Task<IReadOnlyCollection<Message>> FindMessagesByGroupId(Guid id, CancellationToken cancellationToken)
    {
        var messages = await dbContext.Messages.Where(x => x.GroupId == id).ToListAsync(cancellationToken);
        return messages;
    }
}
