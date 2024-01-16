using Froggie.Data.Tasks;
using Froggie.Domain.Users;
using LittleByte.Data;
using LittleByte.AutoMapper.Data;

namespace Froggie.Api.Tasks;

public sealed class GetTasksByUserController(IGetUsersTasksService tasksService, IMapper mapper) : TaskController
{
    [HttpGet("get-user-tasks")]
    [ResponseType(HttpStatusCode.OK, typeof(Page<TaskDto>))]
    [ResponseType(HttpStatusCode.NotFound)]
    public async ValueTask<ApiResponse<Page<TaskDto>>> GetTasksByUser(GetTasksByUserRequest request)
    {
        var userId = new Id<User>(request.UserId);
        var tasks = await tasksService.FindAsync(userId);

        var response = tasks.CastResults<Task, TaskDto>(mapper);
        return new OkResponse<Page<TaskDto>>(response);
    }
}