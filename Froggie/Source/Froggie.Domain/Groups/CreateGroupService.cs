namespace Froggie.Domain.Groups;

internal sealed class CreateGroupService
{
    private readonly IGroupFactory groupFactory;

    public CreateGroupService(IGroupFactory groupFactory)
    {
        this.groupFactory = groupFactory;
    }

    public async ValueTask<Group> CreateAsync(string nameValue)
    {
        var id = new Id<Group>();
        var group = groupFactory.Create(id, nameValue);
        await ValueTask.CompletedTask;
        return group;
    }
}