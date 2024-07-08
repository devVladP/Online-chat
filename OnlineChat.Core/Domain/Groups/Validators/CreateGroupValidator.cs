using FluentValidation;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;
using OnlineChat.Core.Domain.Users.Rules;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class CreateGroupValidator : AbstractValidator<CreateGroupData>
{
    public CreateGroupValidator(IUserMustExistChecker userMustExistChecker)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage("OwnerId is required");

        RuleFor(x => x.OwnerId)
            .CustomAsync(async (ownerId, context, cancellationToken) =>
            {
                var ruleResult = await new UserMustExistRule(ownerId, userMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(User), error));
            });
    }
}
