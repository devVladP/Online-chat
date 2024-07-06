using MediatR;
using PagesResponses;

namespace OnlineChat.Application.Domain.Groups.Queries;

public record GetGroupsQuery(int page, int pageSize) : IRequest<PageResponse<GroupDto[]>>;