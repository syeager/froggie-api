using AutoMapper;
using JetBrains.Annotations;
using LittleByte.AutoMapper;

namespace Froggie.Domain;

[UsedImplicitly]
internal sealed class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        CreateMap<StringValueObject, string>().ConvertUsing<StringValueObjectConverter>();
    }
}