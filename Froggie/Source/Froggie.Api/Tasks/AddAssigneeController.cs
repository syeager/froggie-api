using Froggie.Domain.Tasks;
using Froggie.Domain.Users;

namespace Froggie.Api.Tasks;

public sealed class AddAssigneeController(IAddUserToTaskService addUserToTask, IMapper mapper) : TaskController
{
    [HttpPost("assignee-add")]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public async ValueTask<ApiResponse<TaskDto>> AddAssignee(AddAssigneeRequest request)
    {
        var taskId = new Id<Task>(request.TaskId);
        var userId = new Id<User>(request.UserId);

        var task = await addUserToTask.AddAsync(userId, taskId).NoAwait();

        var dto = mapper.Map<TaskDto>(task);
        return new OkResponse<TaskDto>(dto);
    }
}