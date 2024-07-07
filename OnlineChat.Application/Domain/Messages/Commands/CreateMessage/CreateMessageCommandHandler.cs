using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Messages.Common;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Messages.Models;

namespace OnlineChat.Application.Domain.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageRepository messageRepository
    ) : IRequestHandler<CreateMessageCommand, Guid>
{
    public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateMessageData(request.Content, request.OwnerId, request.GroupId);
        var message = Message.Create(data);

        messageRepository.Add(message);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
