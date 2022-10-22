using AutoMapper;
using Froggie.Data.Users.Models;
using Froggie.Domain.Users.Models;
using JetBrains.Annotations;

namespace Froggie.Data.Users.Mappings;

[UsedImplicitly]
internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDao>().ForMember(dao => dao.UserName, config => config.MapFrom(domain => domain.Name.Value));
        CreateMap<UserDao, User>().ConstructUsing(dao => dao.ToUser());
    }
}