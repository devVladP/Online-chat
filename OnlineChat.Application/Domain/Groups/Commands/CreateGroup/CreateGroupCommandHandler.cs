using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Models;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository
    ) : IRequestHandler<CreateGroupCommand, Guid>
{
    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateGroupData(request.Title, request.OwnerId);
        var group = Group.Create(data);

        groupRepository.Add(group);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}
