using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Domain.Groups.Queries.GetGroupDetails;

public record GroupDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public string OwnerNickName { get; init; }

    public IReadOnlyCollection<Guid> Members { get; init; }
}
