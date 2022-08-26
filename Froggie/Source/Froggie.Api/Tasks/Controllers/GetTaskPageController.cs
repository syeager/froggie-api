using System.Net;
using Froggie.Data.Tasks.Queries;
using LittleByte.Extensions.AspNet.Core;
using LittleByte.Extensions.AspNet.Responses;
using LittleByte.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

public sealed class GetTaskPageController : TaskController
{
    private readonly ITaskPageQuery taskPageQuery;

    public GetTaskPageController(ITaskPageQuery taskPageQuery)
    {
        this.taskPageQuery = taskPageQuery;
    }

    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<Task>))]
    public async Task<ApiResponse<PageResponse<Task>>> GetPage([FromQuery]PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await taskPageQuery.RunQuery(request);
        return new OkResponse<PageResponse<Task>>(response);
    }
}