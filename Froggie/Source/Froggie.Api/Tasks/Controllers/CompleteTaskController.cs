using System.ComponentModel.DataAnnotations;
using LittleByte.Domain;
using LittleByte.EntityFramework;

namespace Froggie.Api.Tasks;

public sealed record CompleteTaskRequest([Required] Guid TaskId);

public sealed class CompleteTaskController(
    IFindByIdQuery<Task> findTask,
    ISaveContextCommand saveCommand,
    IMapper mapper) : TaskController
{
    [HttpPut("complete")]
    public async ValueTask<ApiResponse<TaskDto>> Complete(CompleteTaskRequest request)
    {
        var taskId = request.TaskId.ToId<Task>();
        var task = await findTask.FindRequiredForEditAsync(taskId);

        task.Complete();
        await saveCommand.CommitChangesAsync();

        var dto = mapper.Map<TaskDto>(task);
        return new OkResponse<TaskDto>(dto);
    }
}