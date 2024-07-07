using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Domain.Groups.Commands.UpdateGroup;

internal class UpdateGroupCommandHandler(
    IUnitOfWork unitOfWork,
    IGroupRepository groupRepository
    ) : IRequestHandler<UpdateGroupCommand>
{
    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.FindAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"There is no group with id {request.Id}");

        var data = new UpdateGroupData(request.Title, request.OwnerId);

        group.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
