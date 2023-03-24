using Froggie.Domain.Users;
using JetBrains.Annotations;

namespace Froggie.Api.Users;

[UsedImplicitly]
internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<LogInResult, LogInResponse>();
    }
}