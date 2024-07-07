using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Users.Common;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateUserGroup;

internal class CreateUserGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IUserGroupRepository userGroupRepository,
    IGroupMustExistChecker groupMustExistChecker,
    IUserMustExistChecker userMustExistChecker
    ) : IRequestHandler<CreateUserGroupCommand>
{
    public async Task Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateUserGroupData(request.UserId, request.GroupId);

        var userGroup = await UserGroup.Create(groupMustExistChecker, userMustExistChecker, data, cancellationToken);

        userGroupRepository.Add(userGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken); 
    }
}
