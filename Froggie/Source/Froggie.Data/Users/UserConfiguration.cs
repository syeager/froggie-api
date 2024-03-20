using Froggie.Data.Accounts;
using Froggie.Domain.Users;
using LittleByte.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsersData(this IServiceCollection services) => services
        .AddIdentity()
        .AddSingleton<IUserFactory, UserFactory>()
        .AddScoped<IAddUserCommand, AddUserCommand>()
        .AddScoped<IUserGroupExistsQuery, UserGroupExistsQuery>()
        .AddScoped<IFindByIdQuery<User>, FindByIdQuery<User, FroggieDb>>()
        .AddScoped<IAccountManager, AccountManager>()
        .AddTransient<IFindAccountByEmailQuery, FindAccountByEmailQuery>()
        .AddTransient<IDoesUserWithNameExistQuery, DoesUserWithNameExistQuery>()
        .AddTransient<IUserPageQuery, UserPageQuery>()
        .AddTransient<ICreateAccountCommand, CreateAccountCommand>()
        .AddTransient<IFindAccountByEmailAndPassword, FindAccountByEmailAndPassword>()
    ;

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<Account>(options =>
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