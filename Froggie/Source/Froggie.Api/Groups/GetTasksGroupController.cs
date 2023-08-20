using Froggie.Api.Tasks;
using Froggie.Domain.Groups;

namespace Froggie.Api.Groups;

public sealed class GetTasksGroupController : GroupController
{
    private readonly IGetTasksByGroupQuery getTasksByGroupQuery;

    public GetTasksGroupController(IMapper mapper, IGetTasksByGroupQuery getTasksByGroupQuery)
        : base(mapper)
    {
        this.getTasksByGroupQuery = getTasksByGroupQuery;
    }

    [HttpGet("tasks")]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<TaskDto>))]
    public async ValueTask<ApiResponse<PageResponse<TaskDto>>> GetTasks(Guid id)
    {
        var groupId = new Id<Group>(id);

        var tasks = await getTasksByGroupQuery.QueryAsync(groupId);
        var dtos = tasks.CastResults<TaskDto>(mapper);

        return new OkResponse<PageResponse<TaskDto>>(dtos);
    }
}