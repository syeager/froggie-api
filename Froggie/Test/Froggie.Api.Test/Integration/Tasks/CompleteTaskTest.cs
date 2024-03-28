using Froggie.Api.Tasks;
using Froggie.Api.Test.Integration.Groups;
using Froggie.Domain.Tasks;
using Froggie.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CompleteTaskTestTest : ApiIntegrationTest<CompleteTaskController>
{
    [Test]
    public async ValueTask CompleteNotCompletedTask()
    {
        var group = CreateGroupAndUsersHelper.Create(services, userCount: 1);
        var task = ValidTask.New(group.Users.First(), group);
        GetService<IAddTaskCommand>().Add(task);
        await saveCommand.CommitChangesAsync();

        var request = new CompleteTaskRequest
        {
            TaskId = task.Id
        };

        var response = await controller.Complete(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response);
            Assert.That(response.Obj!.IsCompleted, Is.True);
        });
    }
}