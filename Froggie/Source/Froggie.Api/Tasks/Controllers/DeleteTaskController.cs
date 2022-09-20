using Froggie.Api.Tasks.Requests;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Froggie.Domain.Tasks;

namespace Froggie.Api.Tasks.Controllers;

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
