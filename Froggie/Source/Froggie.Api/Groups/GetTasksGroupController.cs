using Froggie.Api.Tasks;
using Froggie.Data.Groups;
using Froggie.Domain.Groups;
using LittleByte.Data;
using LittleByte.AutoMapper.Data;

namespace Froggie.Api.Groups;

public sealed class GetTasksGroupController(IMapper mapper, IGetTasksByGroupQuery tasksByGroupQuery)
    : GroupController
{
    [HttpGet("tasks")]
    [ResponseType(HttpStatusCode.OK, typeof(Page<TaskDto>))]
    public async ValueTask<ApiResponse<Page<TaskDto>>> GetTasks(Guid id)
    {
        var groupId = new Id<Group>(id);

        var tasks = await tasksByGroupQuery.QueryAsync(groupId);
        var dtos = tasks.CastResults<Task, TaskDto>(mapper);

        return new OkResponse<Page<TaskDto>>(dtos);
    }
}