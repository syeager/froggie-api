using Froggie.Accounts;
using LittleByte.AutoMapper.AspNet;

namespace Froggie.Api.Accounts;

public static class AccountConfiguration
{
    public static IServiceCollection AddAccounts(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddJwtAuthentication(configuration)
            .BindOptions<JwtOptions>(configuration)
            .AddTransient<ICredentialsGenerator, CredentialsGenerator>()
            .AddTransient<ITokenGenerator, TokenGenerator>()
            .AddSingleton<JwtSecurityTokenConverter>()
            .AddAccounts()
        ;
}