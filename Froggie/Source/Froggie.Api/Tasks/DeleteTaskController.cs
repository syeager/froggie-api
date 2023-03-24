using Froggie.Domain.Tasks;
using LittleByte.Common.Infra.Commands;

namespace Froggie.Api.Tasks;

public sealed class DeleteTaskController : TaskController
{
    private readonly IDeleteTaskService deleteTask;
    private readonly ISaveContextCommand saveContext;

    public DeleteTaskController(IDeleteTaskService deleteTask, ISaveContextCommand saveContext)
    {
        this.deleteTask = deleteTask;
        this.saveContext = saveContext;
    }

    [HttpDelete("delete")]
    [ResponseType(HttpStatusCode.OK)]
    public async ValueTask<ApiResponse> Delete(DeleteTaskRequest request)
    {
        var taskId = new Id<Task>(request.Id);

        await deleteTask.DeleteAsync(taskId);
        await saveContext.CommitChangesAsync();

        return new OkResponse();
    }
}