namespace Froggie.Data.Tasks;

internal interface ITitleFilterParams
{
    string? TitleSegment { get; }
}

internal sealed class TitleFilter : TaskFilter<ITitleFilterParams>
{
    protected override bool WillFilter(ITitleFilterParams parameters) => string.IsNullOrWhiteSpace(parameters.TitleSegment) is false;

    protected override IQueryable<Task> FilterInternal(IQueryable<Task> query, ITitleFilterParams parameters)
    {
        var titleSegment = parameters.TitleSegment!.ToLowerInvariant().Trim();
        return query.Where(t => t.Title.Value.ToLowerInvariant().Contains(titleSegment));
    }
}