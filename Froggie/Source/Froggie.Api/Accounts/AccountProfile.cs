using Froggie.Accounts;
using JetBrains.Annotations;

namespace Froggie.Api.Accounts;

[UsedImplicitly]
internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<LogInResult, LogInResponse>();
    }
}