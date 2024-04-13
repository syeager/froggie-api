namespace Froggie.Data.Tasks;

public interface IGroupFilterParams
{
    Guid? GroupId { get; }
}

internal sealed class GroupFilter : TaskFilter<IGroupFilterParams>
{
    protected override bool WillFilter(IGroupFilterParams parameters) => parameters.GroupId is not null;

    protected override IQueryable<Task> FilterInternal(IQueryable<Task> query, IGroupFilterParams parameters)
    {
        var groupId = parameters.GroupId!;
        return query.Where(t => t.GroupId.Value == groupId);
    }
}