using Froggie.Data.Users.Commands;
using Froggie.Data.Users.Models;
using Froggie.Data.Users.Queries;
using Froggie.Domain.Users.Commands;
using Froggie.Domain.Users.Queries;
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
            .AddTransient<IUserPageQuery, UserPageQuery>()
            .AddTransient<IAddUserCommand, AddUserCommand>()
            .AddTransient<IFindUserByEmailAndPasswordQuery, FindUserByEmailAndPasswordQuery>();
    }
}