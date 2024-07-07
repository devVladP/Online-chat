using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Domain.Groups.Commands.UpdateGroup;

public record UpdateGroupCommand(Guid Id, string Title, Guid OwnerId) : IRequest;

