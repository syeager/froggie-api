using System.Net;
using Froggie.Data.Tasks.Queries;
using LittleByte.Extensions.AspNet.Responses;
using LittleByte.Infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

[AllowAnonymous]
public sealed class GetTaskPageController : TaskController
{
    private readonly ITaskPageQuery taskPageQuery;

    public GetTaskPageController(ITaskPageQuery taskPageQuery)
    {
        this.taskPageQuery = taskPageQuery;
    }

    [HttpGet("get-page")]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<Task>))]
    public async Task<ApiResponse<PageResponse<Task>>> GetPage([FromQuery]PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await taskPageQuery.RunQuery(request);
        return new OkResponse<PageResponse<Task>>(response);
    }
}