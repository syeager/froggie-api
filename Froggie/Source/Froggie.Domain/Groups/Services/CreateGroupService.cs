using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface ICreateGroupService
{
    Group Create(User creator, GroupName name);
    Group CreatePersonal(User creator);
}

internal sealed class CreateGroupService(
    IAddGroupCommand addGroupCommand)
    : ICreateGroupService
{
    public Group Create(User creator, GroupName name)
    {
        var id = new Id<Group>();
        var group = Group.Create(id, name, [creator]);
        addGroupCommand.Add(group);

        return group;
    }

    public Group CreatePersonal(User creator) => Create(creator, GroupNameRules.PersonalName);
}