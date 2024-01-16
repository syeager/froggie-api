using LittleByte.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsersDomain(this IServiceCollection services) => services
        .AddSingleton<IUserFactory, UserFactory>()
        .AddTransient<IUserRegisterService, UserRegisterService>()
        .AddTransient<ModelValidator<User>, UserValidator>();
}