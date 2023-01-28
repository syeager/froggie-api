using System.Net;
using Froggie.Api.Tasks;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using LittleByte.Test.AspNet;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CreateTaskTest : ApiIntegrationTest<CreateTaskController>
{
    [Test]
    public async ValueTask CreateTask_Success()
    {
        var group = Valid.Groups.New();
        var user = Valid.Users.New();

        var userGroupCreateCommand = services.GetRequiredService<IUserGroupCreateCommand>();
        userGroupCreateCommand.Create(user, group);

        await saveCommand.CommitChangesAsync();

        var request = new CreateTaskRequest
        {
            Title = Valid.Tasks.Title,
            CreatorId = user.Id,
            DueDate = Valid.Tasks.DueDate,
            GroupId = group.Id
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response, HttpStatusCode.Created);
        Assert.AreEqual(group.Id.Value, response.Obj!.GroupId);
    }
}