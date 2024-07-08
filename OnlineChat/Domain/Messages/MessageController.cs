using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Application.Domain.Messages.Commands.CreateMessage;
using OnlineChat.Application.Domain.Messages.Queries.GetMessages;
using OnlineChat.Common;
using OnlineChat.Domain.Messages.Requests;
using OnlineChat.Infrastructure.SignalR.Hubs;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Domain.Messages;

public class MessageController(IMediator mediator, IHubContext<ChatHub> hubContext) : ApiControllerBase
{
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateMessageAsync(
        [FromBody][Required] CreateMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateMessageCommand(request.Content, request.OwnerId, request.GroupId);
        await hubContext.Clients.Group(request.GroupId.ToString())
            .SendAsync("SendMessageToGroup", request.GroupId, request.OwnerId, request.Content);

        var messageId = await mediator.Send(command, cancellationToken);
        return Created(messageId);
    }

    [HttpGet("messages")]
    [ProducesResponseType(typeof(PageResponse<MessageDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessagesAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMessagesQuery(page, pageSize);
        var groups = await mediator.Send(query, cancellationToken);

        return Ok(groups);
    }
}
