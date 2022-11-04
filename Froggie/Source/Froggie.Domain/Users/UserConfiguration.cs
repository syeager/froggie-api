using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Users;

internal static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection @this) => @this
        .AddSingleton<IUserFactory, UserFactory>()
        .AddTransient<IUserRegisterService, UserRegisterService>()
        .AddTransient<ILogInService, LogInService>();
}