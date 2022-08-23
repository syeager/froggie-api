using AutoMapper;
using Froggie.Data.Tasks.Models;
using LittleByte.Extensions.AutoMapper;

namespace Froggie.Data.Tasks.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.CreateBiDirectionMap<TaskDao, Task>();
    }
}