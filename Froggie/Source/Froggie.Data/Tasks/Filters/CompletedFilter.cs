namespace Froggie.Data.Tasks;

public interface ICompletedFilterParams
{
    bool? IsCompleted { get; }
}

internal sealed class CompletedFilter : TaskFilter<ICompletedFilterParams>
{
    protected override bool WillFilter(ICompletedFilterParams parameters) => parameters.IsCompleted is not null;

    protected override IQueryable<Task> FilterInternal(IQueryable<Task> query, ICompletedFilterParams parameters)
    {
        var isCompleted = parameters.IsCompleted!;
        return query.Where(t => t.IsCompleted == isCompleted);
    }
}