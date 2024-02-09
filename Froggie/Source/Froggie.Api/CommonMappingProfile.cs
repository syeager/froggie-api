using System.IdentityModel.Tokens.Jwt;
using JetBrains.Annotations;
using LittleByte.AutoMapper;
using JwtSecurityTokenConverter = LittleByte.AutoMapper.AspNet.JwtSecurityTokenConverter;

namespace Froggie.Api;

[UsedImplicitly]
public class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        this.MapBothConvertOne<StringValueObject, string, StringValueObjectConverter>();
        CreateMap<JwtSecurityToken?, string?>().ConvertUsing<JwtSecurityTokenConverter>();
    }
}