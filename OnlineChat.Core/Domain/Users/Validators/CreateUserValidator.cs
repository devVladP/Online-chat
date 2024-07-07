using FluentValidation;
using OnlineChat.Core.Domain.Users.Data;

namespace OnlineChat.Core.Domain.Users.Validators;

internal class CreateUserValidator : AbstractValidator<CreateUserData>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MaximumLength(150);
    }
}
