using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Domain.Groups.Queries.GetGroupDetails;
using OnlineChat.Core.Exceptions;
using OnlineChat.Persistence;

namespace OnlineChat.Infrastructure.Application.Domain.Groups.Queries.GetGroupDetails;

internal class GetGroupDetailsQueryaHandler(OnlineChatDbContext dbContext) : IRequestHandler<GetGroupDetailsQuery, GroupDetailsDto>
{
    public async Task<GroupDetailsDto> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
            .AsNoTracking()
            .Where(g => g.Id == request.Id)
            .Include(g => g.Owner)
            .Include(g => g.UserGroups)
            .Select(g => new GroupDetailsDto
            {
                Id = g.Id,
                Title = g.Title,
                OwnerNickName = g.Owner.Nickname,
                Members = g.UserGroups.Select(x => x.UserId).ToList(),
            }).SingleOrDefaultAsync(cancellationToken)
         ?? throw new NotFoundException($"There is no group with Id: {request.Id}");
    }
}
