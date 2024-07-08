using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Domain.Users.Queries.GetUsers;
using OnlineChat.Persistence;
using PagesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.Application.Domain.Users.Queries.GetUsers;

internal class GetUsersQueryHandler(OnlineChatDbContext dbContext) : IRequestHandler<GetUsersQuery, PageResponse<UserDto[]>>
{
    public async Task<PageResponse<UserDto[]>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var skipCount = (request.page - 1) * request.pageSize;
        var query = dbContext.Users.AsNoTracking();

        var users = await query
            .Skip(skipCount)
            .Take(request.pageSize)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Nickname = u.Nickname,
            }).ToArrayAsync(cancellationToken);

        var count = users.Count();

        return new PageResponse<UserDto[]>(count, users);
    }
}
