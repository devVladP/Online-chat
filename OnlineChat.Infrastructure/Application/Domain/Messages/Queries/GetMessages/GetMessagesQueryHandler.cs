using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Domain.Messages.Queries.GetMessages;
using OnlineChat.Persistence;
using PagesResponses;

namespace OnlineChat.Infrastructure.Application.Domain.Messages.Queries.GetMessages;

internal class GetMessagesQueryHandler(OnlineChatDbContext dbContext) : IRequestHandler<GetMessagesQuery, PageResponse<MessageDto[]>>
{
    public async Task<PageResponse<MessageDto[]>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var skipCount = (request.page - 1) * request.pageSize;

        var query = dbContext.Messages.AsNoTracking();

        var messages = await query
            .Skip(skipCount)
            .Take(request.pageSize)
            .Select(m => new MessageDto
            {
                Id = m.Id,
                OwnerId = m.OwnerId,
                GroupId = m.GroupId,
                Content = m.Content,
            }).ToArrayAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PageResponse<MessageDto[]>(count, messages);
    }
}
