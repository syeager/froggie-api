using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Users;

public static class UserConfiguration
{
    public static IServiceCollection AddUsers(this IServiceCollection @this)
    {
        @this
            .AddIdentity<UserDao, IdentityRole<Guid>>(options => options.Password = new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequireLowercase = false,
                RequireUppercase = false,
                RequiredUniqueChars = 0,
                RequireNonAlphanumeric = false
            })
            .AddEntityFrameworkStores<FroggieDb>();

        return @this
            .AddTransient<UserMap>()
            .AddTransient<IUserPageQuery, UserPageQuery>()
            .AddTransient<IAddUserCommand, AddUserCommand>()
            .AddTransient<IFindUserByEmailAndPasswordQuery, FindUserByEmailAndPasswordQuery>();
    }
}