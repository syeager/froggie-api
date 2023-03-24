using Froggie.Api.Groups;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Test;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupTasksTest : ApiIntegrationTest<GetTasksGroupController>
{
    [Test]
    public async ValueTask QueryAllTasks()
    {
        var group = await CreateGroupAndUsersHelper.CreateAsync(services);
        await saveCommand.CommitChangesAsync();

        var users = await GetService<IGetUsersInGroupQuery>().QueryAsync(group);
        var author = users.Results.First();

        await GetService<ICreateTaskService>().CreateAsync(Valid.Tasks.Title, author, Valid.Tasks.DueDate, group);
        await GetService<ICreateTaskService>().CreateAsync(Valid.Tasks.Title, author, Valid.Tasks.DueDate, group);
        await saveCommand.CommitChangesAsync();

        var tasks = await controller.GetTasks(group.Id);

        ApiAssert.IsSuccess(tasks);
        Assert.AreEqual(2, tasks.Obj!.TotalResults);
    }
}