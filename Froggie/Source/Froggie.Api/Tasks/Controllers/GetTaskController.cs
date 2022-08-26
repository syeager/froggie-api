﻿using System.Net;
using AutoMapper;
using Froggie.Api.Tasks.Models;
using LittleByte.Extensions.AspNet.Core;
using LittleByte.Extensions.AspNet.Responses;
using LittleByte.Infra.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

public sealed class GetTaskController : TaskController
{
    private readonly IFindByIdQuery<Task> getTask;
    private readonly IMapper mapper;

    public GetTaskController(IFindByIdQuery<Task> getTask, IMapper mapper)
    {
        this.getTask = getTask;
        this.mapper = mapper;
    }

    [HttpGet(Routes.GetById)]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public async ValueTask<ApiResponse<TaskDto>> GetTask(Guid id)
    {
        var validTask = await getTask.FindRequiredAsync(id);
        var taskDto = mapper.Map<TaskDto>(validTask);
        return new OkResponse<TaskDto>(taskDto);
    }
}