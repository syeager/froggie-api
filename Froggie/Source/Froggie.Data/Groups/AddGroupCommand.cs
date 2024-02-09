using Froggie.Domain.Groups;

namespace Froggie.Data.Groups;

internal sealed class AddGroupCommand(FroggieDb database) : IAddGroupCommand
{
    public void Add(Group group) => database.Groups.Add(group);
}