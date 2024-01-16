using Froggie.Domain.Users;
using LittleByte.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsersData(this IServiceCollection services) => services
        .AddIdentity()
        .AddScoped<IUserGroupExistsQuery, UserGroupExistsQuery>()
        .AddScoped<IFindByIdQuery<User>, FindByIdQuery<User, UserDao, FroggieDb>>()
        .AddTransient<IFindUserByEmailQuery, FindUserByEmailQuery>()
        .AddTransient<IDoesUserWithNameExistQuery, DoesUserWithNameExistQuery>()
        .AddTransient<IUserPageQuery, UserPageQuery>()
        .AddTransient<IAddUserCommand, AddUserCommand>()
        .AddTransient<IFindUserByEmailAndPasswordQuery, FindUserByEmailAndPasswordQuery>()
        .AddTransient<UserConverter>();

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<UserDao>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                    RequiredLength = 0,
                    RequiredUniqueChars = 0
                };
                options.User = new UserOptions();
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<FroggieDb>()
            ;
        return services;
    }
}