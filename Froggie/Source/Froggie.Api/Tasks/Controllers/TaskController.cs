using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Controller = LittleByte.Common.AspNet.Core.Controller;

namespace Froggie.Api.Tasks.Controllers;

[OpenApiTag("Tasks")]
[Route("tasks", Name = "Tasks")]
public abstract class TaskController : Controller { }