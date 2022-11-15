﻿namespace Froggie.Domain.Groups;

internal sealed class CreateGroupService
{
    private readonly IGroupFactory groupFactory;
    private readonly IAddGroupCommand addGroupCommand;

    public CreateGroupService(IGroupFactory groupFactory, IAddGroupCommand addGroupCommand)
    {
        this.groupFactory = groupFactory;
        this.addGroupCommand = addGroupCommand;
    }

    public async ValueTask<Group> CreateAsync(string nameValue)
    {
        var id = new Id<Group>();
        var group = groupFactory.Create(id, nameValue);
        addGroupCommand.Add(group);
        await ValueTask.CompletedTask;
        return group;
    }
}