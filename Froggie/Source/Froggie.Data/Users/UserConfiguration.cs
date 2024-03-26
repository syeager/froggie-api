using Froggie.Domain.Users;
using LittleByte.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsersData(this IServiceCollection services) => services
        .AddSingleton<IUserFactory, UserFactory>()
        .AddScoped<IAddUserCommand, AddUserCommand>()
        .AddScoped<IUserGroupExistsQuery, UserGroupExistsQuery>()
        .AddScoped<IFindByIdQuery<User>, FindByIdQuery<User, FroggieDb>>()
        .AddTransient<IUserPageQuery, UserPageQuery>();
}