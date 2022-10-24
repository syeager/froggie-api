using AutoMapper;
using Froggie.Data.Users.Models;
using Froggie.Domain.Users.Models;
using JetBrains.Annotations;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Users.Mappings;

[UsedImplicitly]
internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        this.MapBoth<User, UserDao, UserMap>();
    }
}