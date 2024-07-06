using FluentValidation;
using OnlineChat.Core.Domain.Groups.Data;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class UpdateGroupValidator : AbstractValidator<UpdateGroupData>
{
    public UpdateGroupValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);
    }
}