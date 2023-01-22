using AutoMapper;
using Froggie.Domain.Groups;

namespace Froggie.Data.Groups;

internal sealed class AddGroupCommand : IAddGroupCommand
{
    private readonly FroggieDb database;
    private readonly IMapper mapper;

    public AddGroupCommand(FroggieDb database, IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }

    public void Add(Group group)
    {
        var dao = mapper.Map<GroupDao>(group);
        database.Groups.Add(dao);
    }
}