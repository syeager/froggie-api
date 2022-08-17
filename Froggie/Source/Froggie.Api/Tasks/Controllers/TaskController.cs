using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Controller = LittleByte.Extensions.AspNet.Core.Controller;

namespace Froggie.Api.Tasks.Controllers;

[Route("tasks", Name = "Tasks")]
[OpenApiTag("Tasks")]
[ApiController]
public abstract class TaskController : Controller { }