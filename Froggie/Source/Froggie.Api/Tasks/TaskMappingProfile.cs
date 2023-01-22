using AutoMapper;
using JetBrains.Annotations;

namespace Froggie.Api.Tasks;

[UsedImplicitly]
internal sealed class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<Task, TaskDto>();
    }
}