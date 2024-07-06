using MediatR;

namespace OnlineChat.Application.Domain.Groups.Commands.CreateGroup;

public record CreateGroupCommand(string Title, Guid OwnerId) : IRequest<Guid>;
