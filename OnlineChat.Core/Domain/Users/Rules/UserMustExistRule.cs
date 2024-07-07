using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Users.Rules;

internal class UserMustExistRule(Guid userId, IUserMustExistChecker userMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await userMustExistChecker.CheckUserMustExistAsync(userId, cancellationToken);
        return Check(exists);
    }
    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failure($"{nameof(User)} was not found. {nameof(User.Id)}: '{userId}'.");
    }
}
