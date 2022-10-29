using AutoMapper;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.MapBoth<TaskDao, Task>();
    }
}