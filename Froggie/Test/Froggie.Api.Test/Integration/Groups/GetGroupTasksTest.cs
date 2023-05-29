using Froggie.Api.Groups;
using Froggie.Data.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupTasksTest : ApiIntegrationTest<GetTasksGroupController>
{
    [Test]
    public async ValueTask QueryAllTasks()
    {
        var (group, users) = await CreateGroupAndUsersHelper.CreateAsync(services);
        await saveCommand.CommitChangesAsync();
        var author = users.First();

        await GetService<ICreateTaskService>().CreateAsync(Valid.Tasks.Title, author, Valid.Tasks.DueDate, group);
        await GetService<ICreateTaskService>().CreateAsync(Valid.Tasks.Title, author, Valid.Tasks.DueDate, group);
        await saveCommand.CommitChangesAsync();

        var tasks = await controller.GetTasks(group.Id);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(tasks);
            Assert.That(tasks.Obj!.TotalResults, Is.EqualTo(2));
        });
    }
}