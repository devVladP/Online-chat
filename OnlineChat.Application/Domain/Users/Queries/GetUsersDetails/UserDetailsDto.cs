using OnlineChat.Application.Domain.Groups.Queries.GetGroups;
using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Application.Domain.Users.Queries.GetUsersDetails;

public record UserDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Nickname { get; init; } = string.Empty;

    public IReadOnlyCollection<string> Messages { get; init; }

    public IReadOnlyCollection<GroupDto> MemberGroups { get; init; }
}
