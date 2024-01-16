using JetBrains.Annotations;
using LittleByte.AutoMapper;

namespace Froggie.Data.Tasks;

[UsedImplicitly]
internal sealed class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        this.MapBothConvertOne<TaskDao, Task, TaskConverter>();
    }
}