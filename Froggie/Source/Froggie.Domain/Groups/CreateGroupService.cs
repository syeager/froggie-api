using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface ICreateGroupService
{
    ValueTask<Group> CreateAsync(User creator, string nameValue);
}

internal sealed class CreateGroupService : ICreateGroupService
{
    private readonly IGroupFactory groupFactory;
    private readonly IAddGroupCommand addGroupCommand;
    private readonly IAddUserToGroupService addUserToGroupService;

    public CreateGroupService(IGroupFactory groupFactory,
                              IAddGroupCommand addGroupCommand,
                              IAddUserToGroupService addUserToGroupService)
    {
        this.groupFactory = groupFactory;
        this.addGroupCommand = addGroupCommand;
        this.addUserToGroupService = addUserToGroupService;
    }

    public async ValueTask<Group> CreateAsync(User creator, string nameValue)
    {
        var id = new Id<Group>();
        var group = groupFactory.Create(id, nameValue);
        addGroupCommand.Add(group);

        await addUserToGroupService.AddAsync(creator, group);

        return group;
    }
}