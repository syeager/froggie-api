using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Accounts;

public static class AccountConfiguration
{
    public static IServiceCollection AddAccounts(this IServiceCollection services) => services
        .AddIdentity()
        .AddTransient<ICreateAccountService, CreateAccountService>()
        .AddScoped<IAccountManager, AccountManager>()
        .AddTransient<IFindAccountByEmailQuery, FindAccountByEmailQuery>()
        .AddTransient<ILogInService, LogInService>()
        .AddTransient<IDoesUserWithNameExistQuery, DoesAccountWithNameExistQuery>()
        .AddTransient<ICreateAccountCommand, CreateAccountCommand>()
        .AddTransient<IFindAccountByEmailAndPassword, FindAccountByEmailAndPassword>()
        .AddTransient<IRegisterUserService, RegisterUserService>()
        .AddDbContext<AccountsDb>(options => options.UseInMemoryDatabase("froggie-accounts"))
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
            .AddEntityFrameworkStores<AccountsDb>()
            ;
        return services;
    }
}