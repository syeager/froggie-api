using AutoMapper;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "UnusedType.Global")]
internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.MapBothConvertOne<TaskDao, Task, TaskMap>();
    }
}