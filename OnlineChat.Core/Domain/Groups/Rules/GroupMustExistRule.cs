using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Models;

namespace OnlineChat.Core.Domain.Groups.Rules;

internal class GroupMustExistRule(Guid groupId, IGroupMustExistChecker groupMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await groupMustExistChecker.CheckGroupMustExistAsync(groupId, cancellationToken);
        return Check(exists);
    }
    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failure($"{nameof(Group)} was not found. {nameof(Group.Id)}: '{groupId}'.");
    }
}
