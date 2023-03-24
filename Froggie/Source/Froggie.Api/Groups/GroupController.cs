using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Froggie.Api.Groups;

[OpenApiTag("Groups")]
[Route("groups", Name = "Groups")]
public abstract class GroupController : Controller
{
    protected readonly IMapper mapper;

    protected GroupController(IMapper mapper)
    {
        this.mapper = mapper;
    }
}