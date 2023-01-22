using Froggie.Api.Tasks;
using Froggie.Domain.Tasks;
using Froggie.Domain.Test;
using LittleByte.Common.Extensions;
using LittleByte.Common.Infra.Commands;
using LittleByte.Test.AspNet;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class GetUserTasksTest : ApiIntegrationTest<GetTasksByUserController>
{
    [Test]
    public async ValueTask GetUserTasks_Success()
    {
        var group = Valid.Groups.New();
        var user = Valid.Users.New();
        var tasks = Valid.Tasks.New(2, user.Id, group.Id);
        var addTaskCommand = services.GetRequiredService<IAddTaskCommand>();
        tasks.ForEach((_, task) => addTaskCommand.Add(task));
        await services.GetRequiredService<ISaveContextCommand>().CommitChangesAsync();

        var request = new GetTasksByUserRequest
        {
            UserId = user.Id,
            Page = 0,
            PageSize = 10,
        };

        var response = await controller.GetTasksByUser(request);

        ApiAssert.IsSuccess(response);
        Assert.AreEqual(tasks.Count, response.Obj!.Results.Count);
    }
}