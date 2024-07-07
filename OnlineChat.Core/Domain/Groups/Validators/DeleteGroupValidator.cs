using FluentValidation;
using OnlineChat.Core.Domain.Groups.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class DeleteGroupValidator : AbstractValidator<DeleteGroupData>
{
    public DeleteGroupValidator(Guid ownerId) 
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .Equal(ownerId)
            .WithMessage("Access denied. You are not the owner of this group");
    }
}

