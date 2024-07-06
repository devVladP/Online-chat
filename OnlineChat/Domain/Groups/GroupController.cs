using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Application.Domain.Groups.Commands.CreateGroup;
using OnlineChat.Application.Domain.Groups.Queries;
using OnlineChat.Common;
using OnlineChat.Constants;
using OnlineChat.Domain.Groups.Requests;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Domain.Groups;

[Route(Routes.Group)]
public class GroupController(IMediator mediator) : ApiControllerBase
{
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateGroupAsync(
        [FromBody][Required] CreateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateGroupCommand(request.Title, request.OwnerId);

        var groupId = await mediator.Send(command, cancellationToken);
        return Created(groupId);
    }

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(PageResponse<GroupDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGroupsAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetGroupsQuery(page, pageSize);

        var groups = await mediator.Send(query, cancellationToken);
        return Ok(groups);
    }
}
