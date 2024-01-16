using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Domain;
using LittleByte.EntityFramework;

namespace Froggie.Api.Tasks;

public sealed class AddAssigneeController(IAddUserToTaskService addUserToTask, IMapper mapper, ISaveContextCommand saveContext) : TaskController
{
    [HttpPost("assignee-add")]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public async ValueTask<ApiResponse<TaskDto>> AddAssignee(AddAssigneeRequest request)
    {
        var taskId = new Id<Task>(request.TaskId);
        var userId = new Id<User>(request.UserId);

        var result = await addUserToTask.AddAsync(userId, taskId).NoAwait();

        switch(result)
        {
            case UserNotInTaskGroup userNotInTaskGroup:
                return new BadRequestResponse<TaskDto>(userNotInTaskGroup.ErrorMessage);
            case UserAddedToTask userAddedToTask:
            {
                await saveContext.CommitChangesAsync();

                var dto = mapper.Map<TaskDto>(userAddedToTask.Task);
                return new OkResponse<TaskDto>(dto);
            }
            default:
                throw new UnexpectedResultException(result);
        }
    }
}