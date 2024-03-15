using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using LittleByte.Common;
using LittleByte.Data;
using LittleByte.EntityFramework;

namespace Froggie.Admin.Accounts;

public static class SeedData
{
    public static async ValueTask AddSeedDataAsync(IServiceProvider serviceProvider)
    {
        var usersQuery = serviceProvider.Get<IUserPageQuery>();
        var users = await usersQuery.RunAsync(new PageRequest());

        if(users.TotalResults == 0)
        {
            var accountService = serviceProvider.Get<RegisterTestAccountService>();
            var result = await accountService.RegisterAsync("user@froggie.com", "Test User", "abc");
            if(result.Succeeded is false)
            {
                // TODO: Throw exception.
                return;
            }

            var user = result.Value;

            var saveCommand = serviceProvider.Get<ISaveContextCommand>();
            // TODO: Write bug for this. We shouldn't need to save here.
            await saveCommand.CommitChangesAsync();

            var groups = await serviceProvider.Get<IGetUserGroupsQuery>().QueryAsync(user);
            var dueDate = serviceProvider.Get<TimeProvider>().GetUtcNow().AddHours(1);
            await serviceProvider.Get<ICreateTaskService>().CreateAsync("Hello world 🌞", user, dueDate, groups.First());
            await saveCommand.CommitChangesAsync();
        }
    }
}