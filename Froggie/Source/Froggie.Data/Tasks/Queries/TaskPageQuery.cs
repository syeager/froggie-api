using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data.Tasks.Queries;

public interface ITaskPageQuery
{
    Task<PageResponse<Task>> RunQuery(PageRequest request);
}

internal sealed class TaskPageQuery : ITaskPageQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public TaskPageQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async Task<PageResponse<Task>> RunQuery(PageRequest request)
    {
        var taskDaos = await froggieDb.Tasks
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync();

        var tasks = taskDaos
            .Select(t => mapper.Map<Task>(t))
            .ToArray();

        var response = new PageResponse<Task>(request.PageSize, request.Page, 0, 0, tasks);
        return response;
    }
}