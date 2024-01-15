using Froggie.Domain.Groups;
using JetBrains.Annotations;
using LittleByte.AutoMapper;

namespace Froggie.Data.Groups;

[UsedImplicitly]
internal sealed class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        this.MapBothConvertOne<GroupDao, Group, GroupConverter>();
    }
}