using Froggie.Domain.Users;
using LittleByte.Common.Infra.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection @this)
    {
        @this
            .AddIdentity<UserDao, IdentityRole<Guid>>(options =>
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
            .AddEntityFrameworkStores<FroggieDb>();

        return @this
            .AddScoped<IUserGroupExistsQuery, UserGroupExistsQuery>()
            .AddTransient<UserConverter>()
            .AddScoped<IFindByIdQuery<User>, FindByIdQuery<User, UserDao, FroggieDb>>()
            .AddTransient<IFindUserByEmailQuery, FindUserByEmailQuery>()
            .AddTransient<IDoesUserWithNameExistQuery, DoesUserWithNameExistQuery>()
            .AddTransient<IUserPageQuery, UserPageQuery>()
            .AddTransient<IAddUserCommand, AddUserCommand>()
            .AddTransient<IFindUserByEmailAndPasswordQuery, FindUserByEmailAndPasswordQuery>();
    }
}