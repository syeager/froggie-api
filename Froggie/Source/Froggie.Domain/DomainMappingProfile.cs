using AutoMapper;
using JetBrains.Annotations;

namespace Froggie.Domain;

[UsedImplicitly]
internal sealed class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        CreateMap<StringValueObject, string>().ConvertUsing<StringValueObjectConverter>();
    }
}