using AutoMapper;
using Froggie.Data.Tasks.Models;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Tasks.Mappings;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.CreateBiDirectionMap<TaskDao, Task>();
    }
}