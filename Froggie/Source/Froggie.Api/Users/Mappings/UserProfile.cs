using AutoMapper;
using Froggie.Api.Users.Models;
using Froggie.Api.Users.Responses;
using Froggie.Domain.Users.Models;
using Froggie.Domain.Users.Results;
using JetBrains.Annotations;

namespace Froggie.Api.Users.Mappings;

[UsedImplicitly]
internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<LogInResult, LogInResponse>();
    }
}