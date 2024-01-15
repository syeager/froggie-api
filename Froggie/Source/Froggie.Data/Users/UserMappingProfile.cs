using Froggie.Domain.Users;
using JetBrains.Annotations;
using LittleByte.AutoMapper;

namespace Froggie.Data.Users;

[UsedImplicitly]
internal sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        this.MapBothConvertBoth<User, UserDao, UserConverter>();
        this.MapBothConvertOne<StringValueObject, string, StringValueObjectConverter>();
    }
}