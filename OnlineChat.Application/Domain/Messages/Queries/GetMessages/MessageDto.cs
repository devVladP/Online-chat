using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Application.Domain.Messages.Queries.GetMessages;

public record MessageDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public Guid OwnerId { get; init; }

    [Required]
    public Guid GroupId { get; init; }

    [Required]
    public string Content { get; init; }
}
