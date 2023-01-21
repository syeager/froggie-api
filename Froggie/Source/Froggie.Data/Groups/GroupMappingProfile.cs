using AutoMapper;
using Froggie.Domain.Groups;
using JetBrains.Annotations;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Groups;

[UsedImplicitly]
internal sealed class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        this.MapBothConvertOne<GroupDao, Group, GroupConverter>();
    }
}