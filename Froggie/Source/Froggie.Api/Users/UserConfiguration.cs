using Froggie.Domain.Users.Services;

namespace Froggie.Api.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection @this)
    {
        return @this
            .AddTransient<IUserRegisterService, UserRegisterService>()
            .AddTransient<ILogInService, LogInService>();
    }
}