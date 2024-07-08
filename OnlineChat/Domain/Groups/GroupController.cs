using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Application.Domain.Groups.Commands.CreateGroup;
using OnlineChat.Application.Domain.Groups.Commands.CreateUserGroup;
using OnlineChat.Application.Domain.Groups.Commands.DeleteGroup;
using OnlineChat.Application.Domain.Groups.Commands.DeleteUserGroup;
using OnlineChat.Application.Domain.Groups.Commands.UpdateGroup;
using OnlineChat.Application.Domain.Groups.Queries.GetGroupDetails;
using OnlineChat.Application.Domain.Groups.Queries.GetGroups;
using OnlineChat.Common;
using OnlineChat.Constants;
using OnlineChat.Domain.Groups.Requests;
using OnlineChat.Infrastructure.SignalR.Hubs;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Domain.Groups;

[Route(Routes.Group)]
public class GroupController(IMediator mediator, IHubContext<ChatHub> hubContext) : ApiControllerBase
{
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroupAsync(
        [FromRoute][Required] Guid id,
        [FromBody][Required] UpdateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateGroupCommand(id, request.Title, request.OwnerId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("groups")]
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

    [HttpGet("details/{id}")]
    [ProducesResponseType(typeof(GroupDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGroupDetailsAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetGroupDetailsQuery(id);
        var group = await mediator.Send(query, cancellationToken);

        return Ok(group);
    }

    [HttpDelete("delete/{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGroupAsync(
        [FromRoute][Required] Guid groupId,
        [FromBody][Required] DeleteGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteGroupCommand(groupId, request.OwnerId);
        var id = await mediator.Send(command, cancellationToken);

        await hubContext.Clients.Groups(groupId.ToString()).SendAsync("NotifyGroupDeleted", groupId);

        return Ok(id);
    }

    [HttpPost("{userId}/{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddUserGroupAsync(
        [FromRoute][Required] Guid userId,
        [FromRoute][Required] Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserGroupCommand(userId, groupId);
        await mediator.Send(command, cancellationToken);

        await hubContext.Clients.Group(groupId.ToString()).SendAsync("JoinGroup", groupId);

        return Ok();
    }

    [HttpDelete("{userId}/{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserGroupAsync(
        [FromRoute][Required] Guid userId,
        [FromRoute][Required] Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserGroupCommand(userId, groupId);
        await mediator.Send(command, cancellationToken);

        await hubContext.Clients.Group(groupId.ToString()).SendAsync("LeaveGroup", groupId);

        return Ok();
    }
}
