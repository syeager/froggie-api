using Froggie.Data.Users;
using Froggie.Domain.Users;

namespace Froggie.Api.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddUsersApi(configuration)
            .AddUsersDomain()
            .AddUsersData();

    private static IServiceCollection AddUsersApi(this IServiceCollection services, IConfiguration configuration) =>
        services
            .BindOptions<JwtOptions>(configuration)
            .AddTransient<ICredentialsGenerator, CredentialsGenerator>()
            .AddTransient<ILogInService, LogInService>()
            .AddTransient<ITokenGenerator, TokenGenerator>();
}