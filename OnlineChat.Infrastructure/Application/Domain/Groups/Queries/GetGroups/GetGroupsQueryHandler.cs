using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Domain.Groups.Queries;
using OnlineChat.Persistence;
using PagesResponses;

namespace OnlineChat.Infrastructure.Application.Domain.Groups.Queries.GetGroups;

internal class GetGroupsQueryHandler(OnlineChatDbContext dbContext) : IRequestHandler<GetGroupsQuery, PageResponse<GroupDto[]>>
{
    public async Task<PageResponse<GroupDto[]>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Groups.AsNoTracking();

        var skipCount = (request.page - 1) * request.pageSize;

        var groups = await query
            .Skip(skipCount)
            .Take(request.pageSize)
            .Select(g => new GroupDto
            {
                Id = g.Id,
                Title = g.Title,
                OwnerId = g.OwnerId,
            })
            .ToArrayAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PageResponse<GroupDto[]>(count, groups);
    }
}
