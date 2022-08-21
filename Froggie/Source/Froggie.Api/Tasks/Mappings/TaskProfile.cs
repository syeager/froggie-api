using AutoMapper;
using Froggie.Api.Tasks.Models;

namespace Froggie.Api.Tasks.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Froggie.Domain.Tasks.Models.Task, TaskDto>();
    }
}