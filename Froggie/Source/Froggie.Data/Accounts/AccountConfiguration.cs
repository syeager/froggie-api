using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Accounts;

public static class AccountConfiguration
{
    public static IServiceCollection AddAccountsData(this IServiceCollection services) => services
        .AddTransient<IAccountRegisterService, AccountRegisterService>();
}