using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Domain.Users.Queries.GetUsersDetails;
using OnlineChat.Persistence;
using OnlineChat.Application.Domain.Groups.Queries.GetGroups;
using OnlineChat.Core.Exceptions;

namespace OnlineChat.Infrastructure.Application.Domain.Users.Queries.GetUserDetails;

internal class GetUserDetailsQueryHandler(OnlineChatDbContext dbContext) : IRequestHandler<GetUserDetailsQuery, UserDetailsDto>
{
    public async Task<UserDetailsDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Include(u => u.UserGroups)
            .ThenInclude(ug => ug.Group)
            .Select(u => new UserDetailsDto
            {
                Id = u.Id,
                Nickname = u.Nickname,
                MemberGroups = u.UserGroups.Select(ug => new GroupDto
                {
                    Id = ug.GroupId,
                    Title = ug.Group.Title,
                    OwnerId = ug.Group.OwnerId,
                }).ToArray(),
            }).SingleOrDefaultAsync(cancellationToken)
         ?? throw new NotFoundException($"There is no user with id: {request.Id}");
    }
}
