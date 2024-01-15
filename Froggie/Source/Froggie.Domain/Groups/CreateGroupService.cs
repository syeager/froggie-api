using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface ICreateGroupService
{
    ValueTask<Group> CreateAsync(User creator, string nameValue);
    ValueTask<Group> CreatePersonalAsync(User creator);
}

internal sealed class CreateGroupService(
    IGroupFactory groupFactory,
    IAddGroupCommand addGroupCommand,
    IAddUserToGroupService addUserToGroupService)
    : ICreateGroupService
{
    public async ValueTask<Group> CreateAsync(User creator, string nameValue)
    {
        var id = new Id<Group>();
        var group = groupFactory.Create(id, nameValue);
        addGroupCommand.Add(group);

        await addUserToGroupService.AddAsync(creator, group);

        return group;
    }

    public ValueTask<Group> CreatePersonalAsync(User creator) => CreateAsync(creator, NameRules.PersonalName);
}