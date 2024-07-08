using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Data;
using OnlineChat.Core.Domain.Groups.Validators;
using OnlineChat.Core.Domain.Messages.Models;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Core.Domain.Groups.Models;

public class Group : Entity
{
    private readonly List<UserGroup> _userGroups = [];
    private readonly List<Message> _messages = [];

    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }

    public User Owner { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups;

    public IReadOnlyCollection<Message> Messages => _messages;


    public async static Task<Group> Create(CreateGroupData data, 
        IUserMustExistChecker userMustExistChecker, 
        CancellationToken cancellationToken)
    {
        await ValidateAsync(new CreateGroupValidator(userMustExistChecker), data, cancellationToken);

        return new Group
        {
            Id = Guid.NewGuid(),
            Title = data.Title,
            OwnerId = data.OwnerId,
        };
    }

    public void Update(UpdateGroupData data)
    {
        Validate(new UpdateGroupValidator(OwnerId), data);

        Title = data.Title;
        OwnerId = data.OwnerId;
    }

    public Guid Delete(DeleteGroupData data)
    {
        Validate(new DeleteGroupValidator(OwnerId), data);

        return data.Id;
    }
}
