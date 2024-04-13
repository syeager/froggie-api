namespace Froggie.Data.Tasks;

public sealed record SearchParams(
    Guid? AssigneeId = null,
    bool? IsCompleted = null,
    Guid? GroupId = null,
    string? TitleSegment = null,
    int PageIndex = 0,
    int PageSize = PageRequest.DefaultPageSize
) : PageRequest(PageIndex, PageSize),
    ITitleFilterParams,
    ICompletedFilterParams,
    IGroupFilterParams,
    IAssigneeFilterParams;

public interface ISearchTasks
{
    ValueTask<Page<Task>> SearchAsync(SearchParams queryParams);
}

internal class SearchTasks(
    FroggieDb froggieDb,
    TaskFilter<ICompletedFilterParams> completedFilter,
    TaskFilter<IGroupFilterParams> groupFilter,
    TaskFilter<IAssigneeFilterParams> assigneeFilter,
    TaskFilter<ITitleFilterParams> titleFilter) : ISearchTasks
{
    public async ValueTask<Page<Task>> SearchAsync(SearchParams queryParams)
    {
        var tasks = await froggieDb.Tasks
            .AsNoTracking()
            .FilterAsync(completedFilter, queryParams)
            .FilterAsync(groupFilter, queryParams)
            .FilterAsync(assigneeFilter, queryParams)
            .FilterAsync(titleFilter, queryParams)
            .GetPagedAsync(queryParams.PageSize, queryParams.PageIndex);

        return tasks;
    }
}