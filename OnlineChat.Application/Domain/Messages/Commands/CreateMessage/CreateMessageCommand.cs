using MediatR;

namespace OnlineChat.Application.Domain.Messages.Commands.CreateMessage;

public record CreateMessageCommand(string Content, Guid OwnerId, Guid GroupId) : IRequest<Guid>;
