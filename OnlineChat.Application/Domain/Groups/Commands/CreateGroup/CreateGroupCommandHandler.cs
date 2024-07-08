using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Users.Common;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository,
    IUserMustExistChecker userMustExistChecker
    ) : IRequestHandler<CreateGroupCommand, Guid>
{
    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateGroupData(request.Title, request.OwnerId);
        var group = await Group.Create(data, userMustExistChecker, cancellationToken);

        groupRepository.Add(group);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}
