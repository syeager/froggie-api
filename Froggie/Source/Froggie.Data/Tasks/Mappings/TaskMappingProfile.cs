using AutoMapper;
using JetBrains.Annotations;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Tasks;

[UsedImplicitly]
internal sealed class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        this.MapBothConvertOne<TaskDao, Task, TaskConverter>();
    }
}