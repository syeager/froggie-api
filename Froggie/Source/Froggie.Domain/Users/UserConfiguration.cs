using Froggie.Domain.Users.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Users;

internal static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection @this)
    {
        return @this
            .AddSingleton<IUserFactory, UserFactory>();
    }
}