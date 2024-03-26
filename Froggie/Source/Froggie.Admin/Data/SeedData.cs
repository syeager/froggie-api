using System.Net;
using Froggie.Accounts;
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

            var (user, personalGroup) = await RegisterTestUser(serviceProvider);
            await saveCommand.CommitChangesAsync();
            await CreateTaskAsync("Hello world 🌞", user, personalGroup, serviceProvider);
            var testGroup = CreateTestGroup(serviceProvider, user);
            await saveCommand.CommitChangesAsync();
            await CreateTaskAsync("Gotta get stuff done", user, testGroup, serviceProvider);
            await saveCommand.CommitChangesAsync();
        }
    }

    private static async ValueTask<(User, Group)> RegisterTestUser(IServiceProvider serviceProvider)
    {
        var accountService = serviceProvider.Get<IRegisterUserService>();
        var result = await accountService.RegisterAsync("user@froggie.com", "Test User", "abc");
        if(result.Succeeded is false)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, "Failed to create SeedData: TestUser");
        }

        return (result.Value.User, result.Value.PersonalGroup);
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