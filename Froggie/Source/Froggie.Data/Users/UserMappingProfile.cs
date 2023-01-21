using AutoMapper;
using Froggie.Domain.Users;
using JetBrains.Annotations;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Users;

[UsedImplicitly]
internal sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        this.MapBothConvertBoth<User, UserDao, UserMap>();
    }
}