using AutoMapper;
using Froggie.Domain.Groups;
using JetBrains.Annotations;

namespace Froggie.Api.Groups;

[UsedImplicitly]
internal sealed class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GroupDto>();
    }
}