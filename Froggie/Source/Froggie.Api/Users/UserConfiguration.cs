using Froggie.Data.Users;
using Froggie.Domain.Users;

namespace Froggie.Api.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services
            .AddUsersDomain()
            .AddUsersData();
}