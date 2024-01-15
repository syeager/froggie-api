namespace Froggie.Data.Tasks;

public interface ITaskPageQuery
{
    Task<Page<Task>> RunAsync(PageRequest request);
}

internal sealed class TaskPageQuery(FroggieDb db, IMapper mapper) : ITaskPageQuery
{
    public async Task<Page<Task>> RunAsync(PageRequest request)
    {
        var taskDaos = await db.Tasks
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync();

        var tasks = taskDaos
            .Select(mapper.Map<Task>)
            .ToArray();

        var response = new Page<Task>(request.PageSize, request.PageIndex, 0, 0, tasks);
        return response;
    }
}