using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Controller = LittleByte.Common.AspNet.Core.Controller;

namespace Froggie.Api.Tasks;

[OpenApiTag("Tasks")]
[Route("tasks", Name = "Tasks")]
public abstract class TaskController : Controller { }