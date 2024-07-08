using FluentValidation;
using OnlineChat.Core.Domain.Messages.Data;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;
using OnlineChat.Core.Domain.Users.Rules;

namespace OnlineChat.Core.Domain.Messages.Validators;

internal class CreateMessageValidator : AbstractValidator<CreateMessageData>
{
    public CreateMessageValidator(IUserMustExistChecker userMustExistChecker)
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage("OwnerId must exist");

        RuleFor(x => x.OwnerId)
            .CustomAsync(async (ownerId, context, cancellationToken) =>
            {
                var ruleResult = await new UserMustExistRule(ownerId, userMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(User), error));
            });
    }
}
