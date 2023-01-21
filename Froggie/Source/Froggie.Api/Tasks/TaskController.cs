using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Froggie.Api.Tasks;

[OpenApiTag("Tasks")]
[Route("tasks", Name = "Tasks")]
public abstract class TaskController : Controller { }