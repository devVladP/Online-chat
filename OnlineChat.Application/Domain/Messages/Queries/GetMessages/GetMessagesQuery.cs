using MediatR;
using PagesResponses;

namespace OnlineChat.Application.Domain.Messages.Queries.GetMessages;

public record GetMessagesQuery(int page, int pageSize) : IRequest<PageResponse<MessageDto[]>>;
