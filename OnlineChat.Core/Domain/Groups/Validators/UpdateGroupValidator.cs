using FluentValidation;
using OnlineChat.Core.Domain.Groups.Data;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class UpdateGroupValidator : AbstractValidator<UpdateGroupData>
{
    public UpdateGroupValidator(Guid ownerId)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .Equal(ownerId)
            .WithMessage("Access denied. You are not the owner of this group");
    }
}