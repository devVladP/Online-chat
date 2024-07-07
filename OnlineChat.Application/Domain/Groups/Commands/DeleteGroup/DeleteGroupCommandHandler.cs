using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;

namespace OnlineChat.Application.Domain.Groups.Commands.DeleteGroup;

internal class DeleteGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository
    ) : IRequestHandler<DeleteGroupCommand, Guid>
{
    public async Task<Guid> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.FindAsync(request.GroupId, cancellationToken);

        var data = new DeleteGroupData(request.GroupId, request.OwnerId);
        var id = group.Delete(data);

        await groupRepository.DeleteAsync(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return id;
    }
}
