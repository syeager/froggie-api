using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Admin.Accounts;

public static class Configuration
{
    public static IServiceCollection AddAdmin(this IServiceCollection services) => services
        .AddTransient<TestUserFactory>()
        .AddTransient<TestCreateAccountService>()
    ;
}