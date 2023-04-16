using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common;
using LittleByte.Common.Database;
using LittleByte.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class SeedData
{
    public static async Task<IApplicationBuilder> AddSeedDataAsync(this IApplicationBuilder @this, IServiceProvider services)
    {
        var froggieDb = services.GetRequiredService<FroggieDb>();

        if(froggieDb.Users.IsEmpty())
        {
            var user = await services.Get<IUserRegisterService>().RegisterAsync("user@froggie.com", "Test User", "abc");
            // TODO: Write bug for this. We shouldn't need to save here.
            await froggieDb.SaveChangesAsync();
            var groups = await services.Get<IGetUsersGroupsQuery>().QueryAsync(user);
            var dueDate = services.Get<IDateService>().UtcNow + TimeSpan.FromHours(1);
            await services.Get<ICreateTaskService>().CreateAsync("Hello world 🌞", user, dueDate, groups.First());
            await froggieDb.SaveChangesAsync();
        }

        return @this;
    }
}