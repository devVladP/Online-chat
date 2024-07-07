using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Application.Domain.Groups.Queries.GetGroups;

public record GroupDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public Guid OwnerId { get; init; }
}
