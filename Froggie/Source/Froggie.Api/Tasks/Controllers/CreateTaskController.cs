using System.Net;
using AutoMapper;
using Froggie.Api.Tasks.Models;
using Froggie.Api.Tasks.Requests;
using Froggie.Domain.Tasks.Services;
using LittleByte.Extensions.AspNet.Responses;
using LittleByte.Infra.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

[Authorize]
public sealed class CreateTaskController : TaskController
{
    private readonly ICreateTaskService createTask;
    private readonly ISaveContextCommand saveContext;
    private readonly IMapper mapper;

    public CreateTaskController(ICreateTaskService createTask, ISaveContextCommand saveContext, IMapper mapper)
    {
        this.createTask = createTask;
        this.saveContext = saveContext;
        this.mapper = mapper;
    }

    [HttpPost("create")]
    [ResponseType(HttpStatusCode.Created, typeof(TaskDto))]
    public async Task<ApiResponse<TaskDto>> Create(CreateTaskRequest request)
    {
        var validTask = await createTask.CreateAsync(request.Title);
        var task = validTask.GetModelOrThrow();

        await saveContext.CommitChangesAsync();

        var dto = mapper.Map<TaskDto>(task);
        return new CreatedResponse<TaskDto>(dto);
    }
}