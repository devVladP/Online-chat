using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Messages.Common;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Messages.Models;
using OnlineChat.Core.Domain.Users.Common;

namespace OnlineChat.Application.Domain.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageRepository messageRepository,
    IUserMustExistChecker userMustExistChecker
    ) : IRequestHandler<CreateMessageCommand, Guid>
{
    public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateMessageData(request.Content, request.OwnerId, request.GroupId);
        var message = await Message.Create(data, userMustExistChecker, cancellationToken);

        messageRepository.Add(message);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
