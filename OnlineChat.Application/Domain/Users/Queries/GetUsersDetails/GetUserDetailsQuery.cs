using MediatR;

namespace OnlineChat.Application.Domain.Users.Queries.GetUsersDetails;

public record GetUserDetailsQuery(Guid Id) : IRequest<UserDetailsDto>;
