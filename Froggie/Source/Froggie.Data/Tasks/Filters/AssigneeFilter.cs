namespace Froggie.Data.Tasks;

public interface IAssigneeFilterParams
{
    Guid? AssigneeId { get; }
}

internal sealed class AssigneeFilter : TaskFilter<IAssigneeFilterParams>
{
    protected override bool WillFilter(IAssigneeFilterParams parameters) => parameters.AssigneeId is not null;

    protected override IQueryable<Task> FilterInternal(IQueryable<Task> query, IAssigneeFilterParams parameters)
    {
        var assigneeId = parameters.AssigneeId!;
        return query.Where(t => t.Assignees.Any(a => a.Id == assigneeId));
    }
}