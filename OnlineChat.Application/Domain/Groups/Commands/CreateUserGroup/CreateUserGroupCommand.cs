using MediatR;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateUserGroup;

public record CreateUserGroupCommand(Guid UserId, Guid GroupId) : IRequest;
