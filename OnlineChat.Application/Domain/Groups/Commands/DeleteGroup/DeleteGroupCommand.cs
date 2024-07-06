using MediatR;

namespace OnlineChat.Application.Domain.Groups.Commands.DeleteGroup;

public record DeleteGroupCommand(Guid GroupId, Guid OwnerId) : IRequest;
