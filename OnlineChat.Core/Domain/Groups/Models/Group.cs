using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Validators;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Groups.Models;

public class Group : Entity
{
    private List<UserGroup> _userGroups = [];

    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }

    public User Owner { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups;

    public static Group Create(CreateGroupData data)
    {
        Validate(new CreateGroupValidator(), data);

        return new Group
        {
            Id = Guid.NewGuid(),
            Title = data.Title,
            OwnerId = data.OwnerId,
        };
    }

    public void Update(UpdateGroupData data)
    {
        Validate(new UpdateGroupValidator(), data);

        Title = data.Title;
        OwnerId = data.OwnerId;
    }
}
