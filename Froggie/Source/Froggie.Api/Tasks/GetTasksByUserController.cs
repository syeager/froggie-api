using System.Net;
using AutoMapper;
using Froggie.Domain.Tasks;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks;

public sealed class GetTasksByUserController : TaskController
{
    private readonly IMapper mapper;
    private readonly IGetUsersTasksService usersTasksService;

    public GetTasksByUserController(IGetUsersTasksService usersTasksService, IMapper mapper)
    {
        this.usersTasksService = usersTasksService;
        this.mapper = mapper;
    }

    [HttpGet("get-user-tasks")]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<TaskDto>))]
    public async ValueTask<ApiResponse<PageResponse<TaskDto>>> GetTasksByUser(GetTasksByUserRequest request)
    {
        var tasks = await usersTasksService.FindAsync(request.UserId);
        var response = tasks.CastResults<TaskDto>(mapper);

        return new OkResponse<PageResponse<TaskDto>>(response);
    }
}