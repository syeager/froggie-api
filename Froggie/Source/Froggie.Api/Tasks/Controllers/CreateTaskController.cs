using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Tasks.Controllers;

public sealed class CreateTaskController : Controller
{
    [HttpPost("create")]
    public Task Create(string title)
    {
       throw new NotImplementedException();
    }
}