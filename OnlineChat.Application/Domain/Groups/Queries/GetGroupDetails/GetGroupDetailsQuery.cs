using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Domain.Groups.Queries.GetGroupDetails;

public record GetGroupDetailsQuery(Guid Id) : IRequest<GroupDetailsDto>;
