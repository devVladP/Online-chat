using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Application.Domain.Groups.Queries;

public record GroupDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public Guid OwnerId { get; init; }
}
