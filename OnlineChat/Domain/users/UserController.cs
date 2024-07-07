using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Application.Domain.Users.Commands.CreateUser;
using OnlineChat.Application.Domain.Users.Queries.GetUsers;
using OnlineChat.Common;
using OnlineChat.Constants;
using OnlineChat.Domain.users.Requests;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Domain.users;

[Route(Routes.User)]
public class UserController(IMediator mediator) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<UserDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUsersQuery(page, pageSize);

        var users = await mediator.Send(query, cancellationToken);
        return Ok(users);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody][Required] CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(request.Nickname);

        var userId = await mediator.Send(command, cancellationToken);
        return Ok(userId);
    }
}
