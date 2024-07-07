using MediatR;

namespace OnlineChat.Application.Domain.Users.Commands.CreateUser;

public record CreateUserCommand(string Nickname) : IRequest<Guid>;