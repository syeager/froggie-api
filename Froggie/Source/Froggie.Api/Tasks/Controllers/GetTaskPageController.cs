using System.Net;
using AutoMapper;
using Froggie.Api.Tasks.Models;
using Froggie.Data.Tasks.Queries;
using LittleByte.Common.AspNet.Core;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

public sealed class GetTaskPageController : TaskController
{
    private readonly ITaskPageQuery taskPageQuery;
    private readonly IMapper mapper;

    public GetTaskPageController(ITaskPageQuery taskPageQuery, IMapper mapper)
    {
        this.taskPageQuery = taskPageQuery;
        this.mapper = mapper;
    }

    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<TaskDto>))]
    public async Task<ApiResponse<PageResponse<TaskDto>>> GetPage([FromQuery]PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await taskPageQuery.RunQuery(request);
        var dtos = response.CastResults(mapper.Map<TaskDto>);
        return new OkResponse<PageResponse<TaskDto>>(dtos);
    }
}