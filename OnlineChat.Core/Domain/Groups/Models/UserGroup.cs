using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Data;
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

    public static UserGroup Create(CreateUserGroupData data)
    {
        //TODO Validate();

        return new UserGroup(data.UserId, data.GroupId);
    }
}
