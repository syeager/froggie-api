using AutoMapper;
using JetBrains.Annotations;

namespace Froggie.Api.Tasks;

[UsedImplicitly]
internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>();
    }
}