using MediatR;
using PagesResponses;

namespace OnlineChat.Application.Domain.Groups.Queries.GetGroups;

public record GetGroupsQuery(int page, int pageSize) : IRequest<PageResponse<GroupDto[]>>;