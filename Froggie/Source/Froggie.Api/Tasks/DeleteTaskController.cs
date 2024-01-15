using Froggie.Domain.Tasks;
using LittleByte.EntityFramework;

namespace Froggie.Api.Tasks;

public sealed class DeleteTaskController(IDeleteTaskService task, ISaveContextCommand context)
    : TaskController
{
    [HttpDelete("delete")]
    [ResponseType(HttpStatusCode.OK)]
    public async ValueTask<ApiResponse> Delete(DeleteTaskRequest request)
    {
        var taskId = new Id<Task>(request.Id);

        await task.DeleteAsync(taskId);
        await context.CommitChangesAsync();

        return new OkResponse();
    }
}