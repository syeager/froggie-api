using Froggie.Api.Tasks;
using Froggie.Api.Test.Integration.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class AddAssigneeTest : ApiIntegrationTest<AddAssigneeController>
{
    [Test]
    public async ValueTask AddFirstAssignee()
    {
        var (users, task) = await CreateUsersAndTask();

        var userId = users[1].Id;

        var request = new AddAssigneeRequest
        {
            TaskId = task,
            UserId = userId,
        };
        var response = await controller.AddAssignee(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response);
            Assert.That(response.Obj!.Assignees.Count, Is.EqualTo(1));
            Assert.That(response.Obj!.Assignees, Contains.Item(userId.Value));
        });
    }

    [Test]
    public async ValueTask AddSecondAssignee()
    {
        var (users, task) = await CreateUsersAndTask();

        var request = new AddAssigneeRequest
        {
            TaskId = task,
            UserId = users[0].Id,
        };
        
        await controller.AddAssignee(request);

        var userId = users[1].Id;
        request = new AddAssigneeRequest
        {
            TaskId = task,
            UserId = userId,
        };
        var response = await controller.AddAssignee(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response);
            Assert.That(response.Obj!.Assignees.Count, Is.EqualTo(2));
            Assert.That(response.Obj!.Assignees, Contains.Item(userId.Value));
        });
    }

    #region Helpers

    private async Task<(IReadOnlyList<User> users, Task task)> CreateUsersAndTask()
    {
        var (group, users) = await CreateGroupAndUsersHelper.CreateAsync(services, userCount: 2);
        await saveCommand.CommitChangesAsync();

        var task = await GetService<ICreateTaskService>().CreateAsync("Test", users[0], Valid.Tasks.DueDate, group);
        await saveCommand.CommitChangesAsync();

        return (users, task);
    }

    #endregion
}