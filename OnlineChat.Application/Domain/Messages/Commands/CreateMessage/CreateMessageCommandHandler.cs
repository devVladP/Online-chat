using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Messages.Common;

namespace OnlineChat.Application.Domain.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageRepository messageRepository
    ) : IRequestHandler<CreateMessageCommand, Guid>
{
    public Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
