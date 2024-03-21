using Froggie.Api.Groups;
using Froggie.Data.Groups;
using Froggie.Domain.Tasks;
using Froggie.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupTasksTest : ApiIntegrationTest<GetTasksGroupController>
{
    [Test]
    public async ValueTask QueryAllTasks()
    {
        var group = CreateGroupAndUsersHelper.Create(services);
        await saveCommand.CommitChangesAsync();

        var users = await GetService<IGetUsersInGroupQuery>().QueryAsync(group);
        var author = users.Results.First();

        await GetService<ICreateTaskService>().CreateAsync(ValidTask.Title, author, ValidTask.DueDate, group);
        await GetService<ICreateTaskService>().CreateAsync(ValidTask.Title, author, ValidTask.DueDate, group);
        await saveCommand.CommitChangesAsync();

        var tasks = await controller.GetTasks(group.Id);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(tasks);
            Assert.That(tasks.Obj!.TotalResults, Is.EqualTo(2));
        });
    }
}