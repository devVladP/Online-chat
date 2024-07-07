using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;

namespace OnlineChat.Application.Domain.Groups.Commands.DeleteUserGroup;

internal class DeleteUserGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IUserGroupRepository userGroupRepository
    ) : IRequestHandler<DeleteUserGroupCommand>
{
    public async Task Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
    {
        await userGroupRepository.DeleteAsync(request.UserId, request.GroupId, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
