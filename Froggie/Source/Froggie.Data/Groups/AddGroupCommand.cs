using Froggie.Domain.Groups;

namespace Froggie.Data.Groups;

internal sealed class AddGroupCommand(FroggieDb database, IMapper mapper) : IAddGroupCommand
{
    public void Add(Group group)
    {
        var dao = mapper.Map<GroupDao>(group);
        database.Groups.Add(dao);
    }
}