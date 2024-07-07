using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Validators;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Groups.Models;

public class UserGroup : Entity
{
    private UserGroup() { }

    public UserGroup(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid GroupId { get; set; }

    public Group Group { get; set; }

    public async static Task<UserGroup> Create(IGroupMustExistChecker groupMustExistChecker,
        IUserMustExistChecker userMustExistChecker,
        CreateUserGroupData data,
        CancellationToken cancellationToken)
    {
        await ValidateAsync(new CreateUserGroupValidator(groupMustExistChecker, userMustExistChecker), data, cancellationToken);

        return new UserGroup(data.UserId, data.GroupId);
    }
}
