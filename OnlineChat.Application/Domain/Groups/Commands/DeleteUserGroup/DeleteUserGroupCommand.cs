using MediatR;

namespace OnlineChat.Application.Domain.Groups.Commands.DeleteUserGroup;

public record DeleteUserGroupCommand(Guid UserId, Guid GroupId) : IRequest;
