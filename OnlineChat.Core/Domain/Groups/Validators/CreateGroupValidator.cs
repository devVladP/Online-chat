using FluentValidation;
using OnlineChat.Core.Domain.Groups.Data;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class CreateGroupValidator : AbstractValidator<CreateGroupData>
{
    public CreateGroupValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);
    }
}
