using AutoMapper;
using Froggie.Api.Tasks.Models;
using JetBrains.Annotations;

namespace Froggie.Api.Tasks.Mappings;

[UsedImplicitly]
internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>();
    }
}