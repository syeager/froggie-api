using AutoMapper;
using Froggie.Domain.Users;
using JetBrains.Annotations;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Users;

[UsedImplicitly]
internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        this.MapBothConvertBoth<User, UserDao, UserMap>();
    }
}