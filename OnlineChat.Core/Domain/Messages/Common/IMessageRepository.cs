using OnlineChat.Core.Domain.Messages.Models;

namespace OnlineChat.Core.Domain.Messages.Common;

public interface IMessageRepository
{
    public Task<Message> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Message>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Message message);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Message>> FindMessagesByUserId(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Message>> FindMessagesByGroupId(Guid id, CancellationToken cancellationToken);
}
