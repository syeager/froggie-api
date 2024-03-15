using System.Net;
using Froggie.Admin.Accounts;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.AspNet;
using LittleByte.Common;
using LittleByte.Data;
using LittleByte.EntityFramework;

namespace Froggie.Admin.Data;

public static class SeedData
{
    public static async ValueTask AddSeedDataAsync(IServiceProvider serviceProvider)
    {
        var usersQuery = serviceProvider.Get<IUserPageQuery>();
        var users = await usersQuery.RunAsync(new PageRequest());

        if(users.TotalResults == 0)
        {
            var saveCommand = serviceProvider.Get<ISaveContextCommand>();

            var user = await CreateTestUser(serviceProvider);
            await saveCommand.CommitChangesAsync();
            var personalGroup = await GetPersonalGroupAsync(user, serviceProvider);
            await CreateTaskAsync("Hello world 🌞", user, personalGroup, serviceProvider);
            var testGroup = CreateTestGroup(serviceProvider, user);
            await saveCommand.CommitChangesAsync();
            await CreateTaskAsync("Gotta get stuff done", user, testGroup, serviceProvider);
            await saveCommand.CommitChangesAsync();
        }
    }

    private static async ValueTask<User> CreateTestUser(IServiceProvider serviceProvider)
    {
        var accountService = serviceProvider.Get<RegisterTestAccountService>();
        var result = await accountService.RegisterAsync("user@froggie.com", "Test User", "abc");
        if(result.Succeeded is false)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, "Failed to create SeedData: TestUser");
        }

        var user = result.Value;
        return user;
    }

    private static async ValueTask<Group> GetPersonalGroupAsync(User user, IServiceProvider serviceProvider)
    {
        var getUserGroupsQuery = serviceProvider.Get<IGetUserGroupsQuery>();
        var groups = await getUserGroupsQuery.QueryAsync(user);
        return groups.First();
    }

    private static Group CreateTestGroup(IServiceProvider serviceProvider, User user)
    {
        var createGroupService = serviceProvider.Get<ICreateGroupService>();
        return createGroupService.Create(user, new GroupName("Test Group"));
    }

    private static async ValueTask CreateTaskAsync(string title, User user, Group group,
                                                   IServiceProvider serviceProvider)
    {
        var createTaskService = serviceProvider.Get<ICreateTaskService>();
        var dueDate = serviceProvider.Get<TimeProvider>().GetUtcNow().AddHours(1);
        var result = await createTaskService.CreateAsync(title, user, dueDate, group);
        if(result.Succeeded is false)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, "Failed to create SeedData: Task");
        }
    }
}