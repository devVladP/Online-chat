using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Users.Data;

namespace OnlineChat.Core.Domain.Users.Models;

public class User : Entity
{
    private readonly List<UserGroup> _userGroups = [];
    private readonly List<Group> _groupsOwner = [];
    public Guid Id { get; private set; }

    public string Nickname { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups  => _userGroups;

    public IReadOnlyCollection<Group> GroupsOwner => _groupsOwner;

    private User() { }

    private User(string nickname)
    {
        Id = Guid.NewGuid();
        Nickname = nickname;
    }

    public static User Create(CreateUserData data)
    {
        //TODO Validate();

        return new User(data.Nickname);
    }

    public void Update(UpdateUserData data)
    {
        //TODO Validate();

        Nickname = data.Nickname;
    }
}
