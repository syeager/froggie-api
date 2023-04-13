using Froggie.Domain.Users;
using LittleByte.Common.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class SeedData
{
    public static IApplicationBuilder AddSeedData(this IApplicationBuilder @this, IServiceProvider services)
    {
        var froggieDb = services.GetRequiredService<FroggieDb>();

        if(froggieDb.Users.IsEmpty())
        {
            var userRegisterService = services.GetRequiredService<IUserRegisterService>();
            _ = userRegisterService.RegisterAsync("user@froggie.com", "Test User", "abc").Result;
            froggieDb.SaveChanges();
        }

        return @this;
    }
}