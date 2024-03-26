using LittleByte.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsersDomain(this IServiceCollection services)
        => services
            .AddTransient<ModelValidator<User>, UserValidator>()
    ;
}