using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public sealed class Group : DomainModel<Group>
{
    private readonly List<DomainModel<User>> users;

    public GroupName Name { get; }
    public IReadOnlyCollection<DomainModel<User>> Users => users;

    private Group(Id<Group> id, GroupName name)
        : base(id)
    {
        Name = name;
        users = [];
    }

    internal static Group Create(Id<Group> id, GroupName name, IEnumerable<DomainModel<User>> users)
    {
        var group = new Group(id, name);
        group.users.AddRange(users);
        var validator = new GroupValidator();
        validator.SignOrThrow(group);
        return group;
    }

    public Result AddUser(User user)
    {
        if(users.Any(u => u.Id == user.Id))
        {
            return new UserAlreadyInGroup();
        }

        users.Add(user);
        return Result.Success();
    }
}