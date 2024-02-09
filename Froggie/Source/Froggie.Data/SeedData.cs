using Froggie.Data.Accounts;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class SeedData
{
    public static async ValueTask AddSeedDataAsync(IServiceProvider services)
    {
        var froggieDb = services.GetRequiredService<FroggieDb>();

        if(froggieDb.Users.IsEmpty())
        {
            var result = await services.Get<IAccountRegisterService>().RegisterAsync("user@froggie.com", "Test User", "abc");
            if(result.Succeeded is false)
            {
                // TODO: Throw exception.
                return;
            }

            var user = result.Value;
            // TODO: Write bug for this. We shouldn't need to save here.
            await froggieDb.SaveChangesAsync();
            var groups = await services.Get<IGetUserGroupsQuery>().QueryAsync(user);
            var dueDate = services.Get<TimeProvider>().GetUtcNow().AddHours(1);
            await services.Get<ICreateTaskService>().CreateAsync("Hello world 🌞", user, dueDate, groups.First());
            await froggieDb.SaveChangesAsync();
        }
    }
}