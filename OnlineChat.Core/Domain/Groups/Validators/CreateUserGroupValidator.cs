using FluentValidation;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Groups.Rules;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;
using OnlineChat.Core.Domain.Users.Rules;

namespace OnlineChat.Core.Domain.Groups.Validators;

internal class CreateUserGroupValidator : AbstractValidator<CreateUserGroupData>
{
    public CreateUserGroupValidator(IGroupMustExistChecker groupMustExistChecker,
        IUserMustExistChecker userMustExistChecker) 
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");

        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("GroupId is required");

        RuleFor(x => x.UserId)
            .CustomAsync(async (userId, context, cancellationToken) =>
            {
                var ruleResult = await new UserMustExistRule(userId, userMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(User), error));
            });

        RuleFor(x => x.GroupId)
            .CustomAsync(async (groupId, context, cancellationToken) =>
            {
                var ruleResult = await new GroupMustExistRule(groupId, groupMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Group), error));
            });
    }
}
