using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.EntityFramework;

namespace Froggie.Api.Tasks;

public sealed class AddAssigneeRequest
{
    [Required]
    public Guid TaskId { get; init; }

    [Required]
    public Guid UserId { get; init; }
}

public sealed class AddAssigneeController(IAddTaskAssigneeService addTaskAssignee, IMapper mapper, ISaveContextCommand saveCommand) : TaskController
{
    [HttpPost("assignee-add")]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public async ValueTask<ApiResponse<TaskDto>> AddAssignee(AddAssigneeRequest request)
    {
        var taskId = new Id<Task>(request.TaskId);
        var userId = new Id<User>(request.UserId);

        var taskResult = await addTaskAssignee.AddAsync(userId, taskId);

        if(!taskResult.Succeeded && taskResult is UserNeedsToBeInTaskGroupToBeAssignee)
        {
            return new BadRequestResponse<TaskDto>(taskResult.ErrorMessage!);
        }

        var dto = mapper.Map<TaskDto>(taskResult.Value);
        await saveCommand.CommitChangesAsync();
        return new OkResponse<TaskDto>(dto);
    }
}