using MediatR;
using PagesResponses;

namespace OnlineChat.Application.Domain.Users.Queries.GetUsers;

public record GetUsersQuery(int page, int pageSize) : IRequest<PageResponse<UserDto[]>>;
