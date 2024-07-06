using FluentValidation;
using OnlineChat.Core.Domain.Messages.Data;

namespace OnlineChat.Core.Domain.Messages.Validators;

internal class CreateMessageValidator : AbstractValidator<CreateMessageData>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
