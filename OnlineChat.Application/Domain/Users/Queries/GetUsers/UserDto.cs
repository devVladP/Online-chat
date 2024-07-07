using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Application.Domain.Users.Queries.GetUsers;

public record UserDto
{
    [Required]
    public Guid Id { get; init; }

    [Required] 
    public string Nickname { get; init; } = string.Empty;
}
