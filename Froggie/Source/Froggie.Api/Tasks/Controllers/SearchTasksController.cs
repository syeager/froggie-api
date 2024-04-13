using Froggie.Data.Tasks;
using LittleByte.Data;

namespace Froggie.Api.Tasks;

// public sealed class SearchTaskRequest(Guid? groupId, Guid? assigneeId, bool? isCompleted, string? titleSegment);

public sealed class SearchTasksController(ISearchTasks searchTasks, IMapper mapper) : TaskController
{
    [HttpGet("search")]
    [ResponseType(HttpStatusCode.OK, typeof(Page<TaskDto>))]
    public async ValueTask<ApiResponse<Page<TaskDto>>> Search([FromQuery] SearchParams request)
    {
        var tasks = await searchTasks.SearchAsync(request);
        var dto = tasks.CastResults(mapper.Map<TaskDto>);
        return new OkResponse<Page<TaskDto>>(dto);
    }
}