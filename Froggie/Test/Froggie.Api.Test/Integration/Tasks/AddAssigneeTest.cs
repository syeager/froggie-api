using Froggie.Api.Tasks;
using Froggie.Api.Test.Integration.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Test;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class AddAssigneeTest : ApiIntegrationTest<AddAssigneeController>
{
    [Test]
    public async ValueTask AddNewAssignee()
    {
        var (group, users) = await CreateGroupAndUsersHelper.CreateAsync(services, userCount: 2);
        await saveCommand.CommitChangesAsync();

        var task = await GetService<ICreateTaskService>().CreateAsync("Test", users[0], Valid.Tasks.DueDate, group);
        await saveCommand.CommitChangesAsync();

        var userId = users[1].Id;

        var request = new AddAssigneeRequest
        {
            TaskId = task,
            UserId = userId,
        };
        var response = await controller.AddAssignee(request);

        ApiAssert.IsSuccess(response);
        CollectionAssert.Contains(response.Obj!.Assignees, userId.Value);
    }
}