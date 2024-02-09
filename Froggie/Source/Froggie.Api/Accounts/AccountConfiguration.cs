using Froggie.Data.Accounts;
using LittleByte.AutoMapper.AspNet;

namespace Froggie.Api.Accounts;

public static class AccountConfiguration
{
    public static IServiceCollection AddAccounts(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddJwtAuthentication(configuration)
            .AddAccountsApi(configuration)
            .AddAccountsData();

    private static IServiceCollection AddAccountsApi(this IServiceCollection services, IConfiguration configuration) =>
        services
            .BindOptions<JwtOptions>(configuration)
            .AddTransient<ICredentialsGenerator, CredentialsGenerator>()
            .AddTransient<ILogInService, LogInService>()
            .AddTransient<ITokenGenerator, TokenGenerator>()
            .AddSingleton<JwtSecurityTokenConverter>()
        ;
}