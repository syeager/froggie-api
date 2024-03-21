using Froggie.Api.Tasks;
using Froggie.Domain.Tasks;
using Froggie.Test;
using LittleByte.AspNet.Test;
using LittleByte.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class GetUserTasksTest : ApiIntegrationTest<GetTasksByUserController>
{
    [Test]
    public async ValueTask GetUserTasks_Success()
    {
        var group = ValidGroup.New();
        var user = ValidUser.New();
        var tasks = ValidTask.New(2, user.Id, group.Id);
        var addTaskCommand = services.GetRequiredService<IAddTaskCommand>();
        tasks.ForEach((task, _) => addTaskCommand.Add(task!));
        await saveCommand.CommitChangesAsync();

        var request = new GetTasksByUserRequest(user.Id);

        var response = await controller.GetTasksByUser(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response);
            Assert.That(response.Obj!.Results.Count, Is.EqualTo(tasks.Count));
        });
    }
}