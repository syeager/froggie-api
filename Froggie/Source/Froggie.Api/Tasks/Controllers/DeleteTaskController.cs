using System.Net;
using Froggie.Domain.Tasks;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;

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
    public async ValueTask<ApiResponse> Delete (DeleteTaskRequest request)
    {
        deleteTask.DeleteAsync(request.Id);

        await saveContext.CommitChangesAsync();

        return new OkResponse();
    }
}
