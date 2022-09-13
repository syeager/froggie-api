using Froggie.Api.Tasks.Requests;
using Froggie.Domain.Tasks.Services;
using LittleByte.Extensions.AspNet.Responses;
using LittleByte.Infra.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    [ResponseType(HttpStatusCode.OK, typeof(bool))]
    public async ValueTask<bool> Delete (DeleteTaskRequest request)
    {
        var result = await deleteTask.DeleteAsync(request.Id);

        await saveContext.CommitChangesAsync();

        // TODO
        return result;
    }
}
