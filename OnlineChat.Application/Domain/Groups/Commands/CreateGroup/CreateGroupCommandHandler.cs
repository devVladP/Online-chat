using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository
    ) : IRequestHandler<CreateGroupCommand, Guid>
{
    public Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
