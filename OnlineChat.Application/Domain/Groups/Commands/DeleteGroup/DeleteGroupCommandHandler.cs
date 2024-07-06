using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;

namespace OnlineChat.Application.Domain.Groups.Commands.DeleteGroup;

internal class DeleteGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository
    ) : IRequestHandler<DeleteGroupCommand>
{
    public Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
